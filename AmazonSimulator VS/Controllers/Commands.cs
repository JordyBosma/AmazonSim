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
        private System.Object parameters;

        public Command(string type, System.Object parameters)
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
        public Model3DCommand(string type, Models.Object3D parameters) : base(type, parameters)
        {
        }
    }

    public class UpdateModel3DCommand : Model3DCommand
    {
        public UpdateModel3DCommand(Models.Object3D parameters) : base("update", parameters)
        {
        }
    }

    /// <summary>
    /// Object with a Object3D data for clients connected requesting to remove this object (from the scene).
    /// </summary>
    public class DeleteModel3DCommand : Model3DCommand
    {
        public DeleteModel3DCommand(Models.Object3D parameters) : base("delete", parameters)
        {
        }
    }

    /// <summary>
    /// Object with nodeGrid data for clients connected requesting to show the display, this so that the client could display the grid with nodes and his conections.
    /// </summary>
    public class ShowGridCommand : Command
    {
        public ShowGridCommand(NodeGrid nodeGrid) : base("grid", nodeGrid)
        {
        }
    }
}