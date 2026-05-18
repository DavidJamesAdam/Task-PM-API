public interface ITaskInterface
{
  Task<IEnumerable<TaskResponseDto>> GetTasksAsync();
  Task<TaskResponseDto?> GetTaskByIdAsync(Guid id);
  Task<IEnumerable<TaskResponseDto?>> GetCreatedTasksForCurrentUserAsync();
  Task<bool> UpdateTaskAsync(Guid id, UpdateTaskDto dto);
  Task<CreateTaskResultDto> CreateTaskAsync(Guid project_id, CreateTaskDto dto);
  Task<bool> DeleteTaskAsync(Guid id);
  Task AssignTaskAsync(Guid task_id, AssignTaskDto dto);
  Task UpdateStatusOfTaskAsync(Guid task_id, UpdateStatusDto dto);
}