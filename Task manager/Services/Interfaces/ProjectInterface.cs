using Task_manager.DTOs;

public interface IProjectService
{
  Task<IEnumerable<ProjectResponseDto>> GetProjectsAsync();
  Task<ProjectResponseDto?> GetProjectByIdAsync(Guid id);
  Task<IEnumerable<TaskResponseDto>> GetTasksByProjectIdAsync(Guid project_id);
  Task<bool> UpdateProjectAsync(Guid id, UpdateProjectDto dto);
  Task<CreateProjectResultDto> CreateProjectAsync(CreateProjectDto dto);
  Task<bool> DeleteProjectAsync(Guid id);
}