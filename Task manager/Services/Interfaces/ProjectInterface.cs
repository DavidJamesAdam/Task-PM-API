using Task_manager.DTOs;

public interface IProjectService
{
  // Task<ProjectResponseDto> GetProjectsAsync();
  // Task<ProjectResponseDto> GetProjectByIdAsync(Guid id);
  // Task<ProjectResponseDto> UpdateProjectAsync(Guid id);
  Task<CreateProjectDto> CreateProjectAsync(CreateProjectDto dto);
  // Task<ProjectResponseDto> DeleteProjectAsync(Guid id);
}