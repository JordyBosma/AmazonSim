class Robot extends THREE.Group {

    constructor() {
        super();

        this.init();
    }

    init() {

        var SelfRef = this;

        var geometry = new THREE.BoxGeometry(0.9, 0.3, 0.9);
        var cubeMaterials = [
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //LEFT
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //RIGHT
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_top.png"), side: THREE.DoubleSide }), //TOP
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //FRONT
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //BACK
        ];
        var material = new THREE.MeshFaceMaterial(cubeMaterials);
        var model = new THREE.Mesh(geometry, material);
        model.position.y = 0.151;
        SelfRef.add(model);
    }
}

class Rocket extends THREE.Group {
    
    constructor() {
        super();

        this.init();
    }

    init() {
        var SelfRef = this;

        loadOBJModel("models/", "Export_Rocket.obj", "textures/Materials/", "Export_Rocket.mtl", (mesh) => {
            mesh.scale.set(1.5, 1.5, 1.5);
            SelfRef.add(mesh);
        });
    }
}

class Train extends THREE.Group {

    constructor() {
        super();

        this.init();
    }

    init() {
        var SelfRef = this;

        loadOBJModel("models/", "ImportTrain.obj", "textures/Materials/", "ImportTrain.mtl", (mesh) => {
            mesh.scale.set(0.85, 0.85, 0.85);
            SelfRef.add(mesh);
        });
    }
}

class Crate extends THREE.Group {

    constructor(command) {
        super();

        this.init(command);
    }

    init(command) {
        var SelfRef = this;
        var CrateInv;

        switch (command.parameters.invetory) {
            
            case "MoonMilk":
                if (command.parameters.refined) {
                    CrateInv = "Crate_Milk_Refined.mtl";
                }
                else {
                    CrateInv = "Crate_Milk.mtl";
                    //debugger;
                }
                break;
            case "Krypto":
                if (command.parameters.refined) {
                    CrateInv = "Crate_Krypto_Refined.mtl";
                }
                else {
                    CrateInv = "Crate_Krypto.mtl";
                    //debugger;
                }
                break;
            case "Beryllium":
                if (command.parameters.refined) {
                    CrateInv = "Crate_Beryl_Refined.mtl";
                }
                else {
                    CrateInv = "Crate_Beryl.mtl";
                    //debugger;
                }
                break;
            case "Uranium":
                if (command.parameters.refined) {
                    CrateInv = "Crate_Uranium_Refined.mtl";
                }
                else {
                    CrateInv = "Crate_Uranium.mtl";
                    //debugger;
                }
                break;
            case "Moonrock":
                if (command.parameters.refined) {
                    CrateInv = "Crate_Moonrock_Refined.mtl";
                }
                else {
                    CrateInv = "Crate_Moonrock.mtl";
                    //debugger;
                }
                break;
            default:
                CrateInv = "Crate.mtl";
                break;
        }

        loadOBJModel("models/", "Crate.obj", "textures/Materials/", "Crate_Milk.mtl", (mesh) => {
            mesh.scale.set(1, 1, 1);
            SelfRef.add(mesh);
        });
    }
}

class Sun extends THREE.Group {

    constructor() {
        super();

        this.init();
    }

    init() {
        var SelfRef = this;

        loadOBJModel("models/", "Sun.obj", "textures/Materials/", "Sun.mtl", (mesh) => {
            mesh.scale.set(1, 1, 1);
            var sunLight = new THREE.DirectionalLight(0xffffff, 0.9);
            var sunRadiance = new THREE.PointLight(0xffa500, 110, 300);
            mesh.add(sunLight, sunRadiance);
            SelfRef.add(mesh);
        });
    }
}

class Earth extends THREE.Group {

    constructor() {
        super();

        this.init();
    }

    init() {
        var SelfRef = this;

        loadOBJModel("models/", "Earthchan.obj", "textures/Materials/", "Earthchan.mtl", (mesh) => {
            mesh.scale.set(1, 1, 1);
            SelfRef.add(mesh);
        });
    }
}

class Dome extends THREE.Group {

    constructor() {
        super();

        this.init();
    }

    init() {
        var SelfRef = this;

        loadOBJModel("models/", "Dome.obj", "textures/Materials/", "Dome.mtl", (mesh) => {
            mesh.scale.set(1, 1, 1);
            SelfRef.add(mesh);
        });
    }
}


/**
 * Load an OBJ model from the server
 * @param {string} objPath The path to the model on the server
 * @param {string} objName The name of the model inside the path (OBJ file)
 * @param {string} materialPath The path to the texture of the model
 * @param {string} materialName The name of the texture of the mdoel (MTL File)
 * @param {function(THREE.Mesh): void} onload The function to be called once the model is loaded and available
 * @return {void}
*/
function loadOBJModel(objPath, objName, materialPath, materialName, onload) {
    new THREE.MTLLoader()
        .setPath(materialPath)
        .load(materialName, function (materials) {

            materials.preload();

            new THREE.OBJLoader()
                .setPath(objPath)
                .setMaterials(materials)
                .load(objName, function (object) {
                    onload(object);
                }, function () { }, function (e) { console.log("Error loading model"); console.log(e); });
        });
}