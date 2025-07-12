using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShippingAPI.DTOS.Permissions;
using ShippingAPI.Interfaces.Permissions;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Services.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermissionService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
     public async Task<int> CreatePermissionAsync(PermissionDto dto)
        {
            var permission = _mapper.Map<Permission>(dto);
            await _unitOfWork.PermissionRepo.AddAsync(permission);
            await _unitOfWork.SaveAsync();
            return permission.Id;
        }
        

        public async Task<int> CreateActionTypeAsync(ActionTypeDto dto)
        {
            var action = _mapper.Map<ActionType>(dto);
            await _unitOfWork.ActionPermissionRepo.AddAsync(action);
            await _unitOfWork.SaveAsync();
            return action.Id;
        }

        public async Task<int> CreatePermissionActionAsync(PermissionActionDto dto)
        {
            var permission = await _unitOfWork.PermissionRepo.GetByIdAsync(dto.PermissionId);
            if (permission == null) throw new Exception("Permission not found.");

            foreach (var actionId in dto.ActionTypeIds)
            {
                var permissionAction = new PermissionAction
                {
                    PermissionId = dto.PermissionId,
                    ActionTypeId = actionId
                };
                await _unitOfWork.PermissionActionRepo.AddAsync(permissionAction);
            }

            await _unitOfWork.SaveAsync();
            return dto.PermissionId;
        }

        public async Task<bool> AssignPermissionToUserAsync(AssignUserPermissionDto dto)
        {
            var permissionActions = await _unitOfWork.PermissionActionRepo
               .WhereAsync(pa => pa.PermissionId == dto.PermissionId);

            foreach (var permAction in permissionActions)
            {
                var exists = await _unitOfWork.UserPermissionRepo.AnyAsync(up =>
                    up.UserId == dto.UserId && up.PermissionActionId == permAction.Id);

                if (!exists)
                {
                    await _unitOfWork.UserPermissionRepo.AddAsync(new UserPermission
                    {
                        UserId = dto.UserId,
                        PermissionActionId = permAction.Id
                    });
                }
            }            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> HasPermissionAsync(CheckPermissionDto dto)
        {
            var permission = await _unitOfWork.PermissionRepo
                .FirstOrDefaultAsync(p => p.Name == dto.PermissionName);
            if (permission == null) return false;

            var permissionActions = await _unitOfWork.PermissionActionRepo
                .FindAsync(pa => pa.PermissionId == permission.Id);

            if (!permissionActions.Any()) return false;

            foreach (var pa in permissionActions)
            {
                var exists = await _unitOfWork.UserPermissionRepo
                    .AnyAsync(up => up.UserId == dto.UserId && up.PermissionActionId == pa.Id);
                if (exists)
                    return true;
            }

            return false;
        }

        public async Task<List<PermissionDto>> GetAllPermissionsAsync()
        {
            var permissions = await _unitOfWork.PermissionRepo.GetAllAsync();
            return _mapper.Map<List<PermissionDto>>(permissions);
        }

        public async Task<PermissionDto?> GetPermissionByIdAsync(int id)
        {
            var permission = await _unitOfWork.PermissionRepo.GetByIdAsync(id);
            return permission == null ? null : _mapper.Map<PermissionDto>(permission);
        }

        public async Task<List<ActionTypeDto>> GetAllActionsAsync()
        {
            var actions = await _unitOfWork.ActionPermissionRepo.GetAllAsync();
            return _mapper.Map<List<ActionTypeDto>>(actions);
        }

        public async Task<ActionTypeDto?> GetActionByIdAsync(int id)
        {
            var action = await _unitOfWork.ActionPermissionRepo.GetByIdAsync(id);
            return action == null ? null : _mapper.Map<ActionTypeDto>(action);
        }
        public async Task<bool> UpdatePermissionActionsAsync(UpdatePermissionActionsDto dto)
        {
            var currentActions = await _unitOfWork.PermissionActionRepo.FindAsync(
                pa => pa.PermissionId == dto.PermissionId);

            var currentActionTypeIds = currentActions.Select(pa => pa.ActionTypeId).ToList();
            var toAdd = dto.ActionTypeIds.Except(currentActionTypeIds).ToList();

            foreach (var actionId in toAdd)
            {
                await _unitOfWork.PermissionActionRepo.AddAsync(new PermissionAction
                {
                    PermissionId = dto.PermissionId,
                    ActionTypeId = actionId
                });
            }
            var toRemove = currentActions.Where(pa => !dto.ActionTypeIds.Contains(pa.ActionTypeId)).ToList();

            foreach (var oldAction in toRemove)
            {
                var relatedUserPermissions = await _unitOfWork.UserPermissionRepo.FindAsync(
                    up => up.PermissionActionId == oldAction.Id);

                foreach (var up in relatedUserPermissions)
                {
                    _unitOfWork.UserPermissionRepo.Delete(up.Id);
                }

                _unitOfWork.PermissionActionRepo.Delete(oldAction.Id);
            }

            await _unitOfWork.SaveAsync();
            return true;
        }
        public async Task<bool> RemovePermissionActionsAsync(RemovePermissionActionsDto dto)
        {
            var existingActions = await _unitOfWork.PermissionActionRepo.FindAsync(
                pa => pa.PermissionId == dto.PermissionId && dto.ActionTypeIds.Contains(pa.ActionTypeId));

            if (!existingActions.Any()) return false;

            foreach (var action in existingActions)
            {
                _unitOfWork.PermissionActionRepo.Delete(action.Id);
            }

            await _unitOfWork.SaveAsync();
            return true;
        }


        public async Task<PermissionWithActionsDto?> GetActionsByPermissionIdAsync(int permissionId)
        {
            var permission = await _unitOfWork.PermissionRepo.FirstOrDefaultAsync(p => p.Id == permissionId);

            if (permission == null)
                return null;

            var dto = new PermissionWithActionsDto
            {
                PermissionId = permission.Id,
                PermissionName = permission.Name,
                Actions = permission.PermissionActions
                    .Select(pa => new ActionTypeDto
                    {
                        Id = pa.ActionType.Id,
                        Name = pa.ActionType.Name
                    }).ToList()
            };

            return dto;
        }

    }
}
