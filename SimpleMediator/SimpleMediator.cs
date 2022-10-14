using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern
{
    public class SimpleMediator
    {
        IServiceProvider _provider;
        Dictionary<string, Type> handlers;
        public SimpleMediator(IServiceProvider provider)
        {
            _provider = provider;
            handlers = new Dictionary<string, Type>();
            handlers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttribute<HandleCommandOfTypeAttribute>() != null)
                .ToDictionary(t => t.Name, t => t);
        }

        public CommandResult Send<TCommand>(TCommand command) where TCommand: ICommand        
        {
            try
            {
                var handlerType = typeof(ICommandHandler<>);
                var genericHandlerType = handlerType.MakeGenericType(typeof(TCommand));
                
                ICommandHandler<TCommand> handler = null; // (ICommandHandler<TCommand>)(Activator.CreateInstance(handlerImplementation));
                //using (var scope = _provider.CreateScope())
                //{
                    //handler = (ICommandHandler<TCommand>)GetCommandHandler(genericHandlerType);
                    
                    //handler = (ICommandHandler<TCommand>)scope.ServiceProvider.GetRequiredService(genericHandlerType);

                handler = (ICommandHandler<TCommand>)GetCommandHandlerFromAttribute(command.GetType());
                var validator = handler.GetType().GetCustomAttribute<HandleCommandOfTypeAttribute>().Validator;
                if(validator != null)
                {
                    var validatorInstance = (ICommandValidator<TCommand>)Activator.CreateInstance(validator, handler);
                    var validationResult = ((ICommandHandler<TCommand>)validatorInstance).Handle(command);
                    return validationResult;
                }
                return handler.Handle(command);
                //}
            }
            catch (Exception ex)
            {
                return new CommandResult() { State = State.Failed };
            }            
        }

        private TCommandHandler getCommandHandlerType<TCommandHandler>()
        {
            using(var scope = _provider.CreateScope())
            {
                return scope.ServiceProvider.GetRequiredService<TCommandHandler>();
            }
        }

        private object GetCommandHandler(Type handlerType)
        {
            var getCommandHandlerMethod = typeof(SimpleMediator).GetMethod(nameof(getCommandHandlerType), BindingFlags.Instance | BindingFlags.NonPublic);
            var genericGetCommandHandlerMethod = getCommandHandlerMethod.MakeGenericMethod(handlerType);
            var result = genericGetCommandHandlerMethod.Invoke(this, null);
            return result;
        }

        private object GetCommandHandlerFromAttribute(Type commandType)
        {
            var resolved = handlers.Values.FirstOrDefault(v => v.GetCustomAttribute<HandleCommandOfTypeAttribute>()?.CommandType == commandType);
            // var handlerType = resolved.GetCustomAttribute<CommandHandlerAttribute>()?.HandlerType;
            return Activator.CreateInstance(resolved);
        }
    }
}
