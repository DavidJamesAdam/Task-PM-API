public interface ITaskInterface
{
  Task<IEnumerable<TaskResponseDto>> GetTasksAsync();
  Task<TaskResponseDto?> GetTaskByIdAsync(Guid id);
  Task<bool> UpdateTaskAsync(Guid id, UpdateTaskDto dto);
  Task<CreateTaskResultDto> CreateTaskAsync(Guid project_id, CreateTaskDto dto);
  Task<bool> DeleteTaskAsync(Guid id);
}