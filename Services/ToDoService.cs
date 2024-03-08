using firstproject.Exceptions;
using firstproject.Models.Domain;
using firstproject.Models.DTOs;
using firstproject.Repositories;

namespace firstproject.Services
{
    public class ToDoService 
    {
        public ToDoRepository _toDoRepository;

        public ToDoService (ToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<ToDo[]> GetAllTasks()
        {
            return await _toDoRepository.GetAll();
        }

        public async Task<ToDo> Create(CreateToDoDto data, long userId)
        {
            ToDo newToDo = data.toEntity();
            newToDo.UserId = userId;

            return await _toDoRepository.Create(newToDo);
        }

        public async Task<List<ToDo>> GetAllFromUser(long userId)
        {
            return await _toDoRepository.GetAllFromUser(userId);
        }

        public async Task<ToDo> Toggle(ToggleToDoDto dto, long userId)
        {
            ToDo? toDo = await _toDoRepository.GetById(dto.ToDoId);

            if(toDo == null) throw new ToDoNotFoundException("To Do not found!");

            if(toDo.UserId != userId) throw new UnauthorizedException("You don't have the access!");

            return await _toDoRepository.Toggle(toDo);
        }


    }
}
