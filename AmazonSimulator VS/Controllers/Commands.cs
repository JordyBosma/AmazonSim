using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Newtonsoft.Json;

namespace Controllers
{
    public abstract class Command
    {
        private string type;
        private Object parameters;

        public Command(string type, Object parameters)
        {
            this.type = type;
            this.parameters = parameters;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(new {
                command = type,
                parameters = parameters
            });
        }
    }

    public abstract class Model3DCommand : Command
    {
        public Model3DCommand(string type, Object3D parameters) : base(type, parameters)
        {
        }
    }

    public class UpdateModel3DCommand : Model3DCommand
    {
        public UpdateModel3DCommand(Object3D parameters) : base("update", parameters)
        {
        }
    }

    public class RobotTask
    {
        private List<double[]> _pickupTask;
        private List<double[]> _dropoffTask;

        public List<double[]> pickupTask { get { return _pickupTask; } }
        public List<double[]> dropoffTask { get { return _dropoffTask; } }

        public RobotTask(List<double[]> pickupTask, List<double[]> dropoffTask)
        {
            this._pickupTask = pickupTask;
            this._dropoffTask = dropoffTask;
        }
    }

    public class RobotRequest
    {
        public double[] currentPosision;
        public Guid id;

        public RobotRequest(double[] Position, Guid id)
        {
            currentPosision = Position;
            this.id = id;
        }
    }

    public static class LogicTask
    {
        public static List<RobotRequest> newRobotTaskRequest;
    }
}