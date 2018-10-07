/**
 * All the classes in this js file create an object that extends THREE.Group.
 * in the init the model is loaded and further tweaks are done.
 * This than gets added to the SelfRef/object.
 * */
class Robot extends THREE.Group {

    constructor() {
        super();

        this.init();
    }

    init() {

        var SelfRef = this;

        var geometry = new THREE.BoxGeometry(0.9, 0.3, 0.9);
        var cubeMaterials = [
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //LEFT
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //RIGHT
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("textures/robot_top.png"), side: THREE.DoubleSide }), //TOP
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //FRONT
            new THREE.MeshLambertMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //BACK
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

class Shelf extends THREE.Group {

    constructor() {
        super();

        this.init();
    }

    init() {
        var SelfRef = this;

        loadOBJModel("models/", "Stellage.obj", "textures/Materials/", "Stellage.mtl", (mesh) => {
            mesh.scale.set(1, 1, 1);
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
        var CrateMtl;
        var CrateObj;


        switch (command.parameters.invetory) {

            case "MoonMilk":
                if (command.parameters.refined) {
                    CrateObj = "Crate_Milk_Refined.obj";
                    CrateMtl = "Crate_Milk_Refined.mtl";
                }
                else {
                    CrateObj = "Crate_Milk.obj";
                    CrateMtl = "Crate_Milk.mtl";
                    //debugger;
                }
                break;
            case "Krypto":
                if (command.parameters.refined) {
                    CrateObj = "Crate_Krypto_Refined.obj";
                    CrateMtl = "Crate_Krypto_Refined.mtl";
                }
                else {
                    CrateObj = "Crate_Krypto.obj";
                    CrateMtl = "Crate_Krypto.mtl";
                    //debugger;
                }
                break;
            case "Beryllium":
                if (command.parameters.refined) {
                    CrateObj = "Crate_Beryl_Refined.obj";
                    CrateMtl = "Crate_Beryl_Refined.mtl";
                }
                else {
                    CrateObj = "Crate_Beryl.obj";
                    CrateMtl = "Crate_Beryl.mtl";
                    //debugger;
                }
                break;
            case "Uranium":
                if (command.parameters.refined) {
                    CrateObj = "Crate_Uranium_Refined.obj";
                    CrateMtl = "Crate_Uranium_Refined.mtl";
                }
                else {
                    CrateObj = "Crate_Uranium.obj";
                    CrateMtl = "Crate_Uranium.mtl";
                    //debugger;
                }
                break;
            case "Moonrock":
                if (command.parameters.refined) {
                    CrateObj = "Crate_Moonrock_Refined.obj";
                    CrateMtl = "Crate_Moonrock_Refined.mtl";
                }
                else {
                    CrateObj = "Crate_Moonrock.obj";
                    CrateMtl = "Crate_Moonrock.mtl";
                    //debugger;
                }
                break;
            default:
                CrateObj = "Crate.obj"
                CrateMtl = "Crate.mtl";
                break;
        }

        loadOBJModel("models/", CrateObj, "textures/Materials/Crates/", CrateMtl, (mesh) => {
            mesh.scale.set(1, 1, 1);
            SelfRef.add(mesh);
        });
    }
}

/**
 * This class creates the sun object, wich also creates the directional light that lights our scene, and casts shadows
 * this light is added to our sun mesh, wich in turn gets loaded in our scene. Our sun object is literaly our source of light.
 * */
class Sun extends THREE.Group {

    constructor() {
        super();

        this.init();
    }

    init() {
        var SelfRef = this;

        loadOBJModel("models/", "Sun.obj", "textures/Materials/", "Sun.mtl", (mesh) => {

            SelfRef.position.x = -2000;
            SelfRef.position.z = -1600;
            SelfRef.position.y = 300;

            // Create Sunlight
            var sunlight = new THREE.DirectionalLight(0xffffff, 1);
            sunlight.position.set(-1000, 300, -600);
            sunlight.target.position.set(0, 0, 0);

            // Create sunlight shadows
            sunlight.castShadow = true;
            sunlight.shadowDarkness = 0.5;
            sunlight.shadowMapWidth = sunlight.shadowMapHeight = 2048;
            sunlight.bias = -0.001;

            var d = 900;

            sunlight.shadowCameraLeft = -d;
            sunlight.shadowCameraRight = d;
            sunlight.shadowCameraTop = d;
            sunlight.shadowCameraBottom = -d;

            sunlight.shadowCameraNear = 0.1;
            sunlight.shadowCameraFar = 10000;

            mesh.add(sunlight.target, sunlight);
            mesh.scale.set(1, 1, 1);
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

            SelfRef.position.x = -1200;
            SelfRef.position.z = -1100;
            SelfRef.position.y = 150;
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

            //Create and load domelights
            var intensity = 1.7;
            var reach = 16;
            var color = 0xffffe0;

            var domeLight1 = new THREE.SpotLight(color, intensity, reach);
            var domeLight2 = new THREE.SpotLight(color, intensity, reach);
            var domeLight3 = new THREE.SpotLight(color, intensity, reach);
            var domeLight4 = new THREE.SpotLight(color, intensity, reach);

            domeLight1.position.set(9, 10, -9);
            domeLight2.position.set(9, 10, 9);
            domeLight3.position.set(-9, 10, 9);
            domeLight4.position.set(-9, 10, -9);

            domeLight1.target.position.set(domeLight1.position.x, 0, domeLight1.position.z);
            domeLight2.target.position.set(domeLight2.position.x, 0, domeLight2.position.z);
            domeLight3.target.position.set(domeLight3.position.x, 0, domeLight3.position.z);
            domeLight4.target.position.set(domeLight4.position.x, 0, domeLight4.position.z);

            mesh.add(domeLight1.target, domeLight2.target, domeLight3.target, domeLight4.target, domeLight1, domeLight2, domeLight3, domeLight4);
            mesh.scale.set(1, 1, 1);
            SelfRef.add(mesh);

            SelfRef.position.x = 0;
            SelfRef.position.z = 0;
        });
    }
}


class Plane extends THREE.Group {
	
	    constructor() {
        super();

        this.init();
		}
		
		init() {
			var SelfRef = this;
			
			var geometry = new THREE.PlaneGeometry(4000, 2000, 32);
			var material = new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("/textures/8k_moon.png"), side: THREE.DoubleSide });
			var plane = new THREE.Mesh(geometry, material);
			plane.rotation.x = Math.PI / 2.0;
			plane.position.x = 15;
			plane.position.z = 15;
			plane.position.y = -0.07;
			
			plane.receiveShadow = true;
			SelfRef.add(plane);
		}
    }
	
class TrainStation extends THREE.Group {

    constructor() {
        super();

        this.init();
    }

    init() {
        var SelfRef = this;

        loadOBJModel("models/", "TrainStation.obj", "textures/Materials/", "TrainStation.mtl", (mesh) => {
            mesh.scale.set(1, 1, 1);
            SelfRef.add(mesh);
            SelfRef.position.x = 600;
            SelfRef.position.z = 34;
            SelfRef.rotation.y = Math.PI;
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
                    //Cast shadows
                    if (objName != "Sun.obj" && objName != "Earthchan.obj") {
                        object.traverse(function (child) {
                            if (objName == "Dome.obj" || objName == "TrainStation.obj") {
                                child.castShadow = true;
                                child.receiveShadow = false;
                            }
                            else {
                                child.castShadow = true;
                                child.receiveShadow = true;
                            }
                        });
                    }
                }, function () { }, function (e) { console.log("Error loading model"); console.log(e); });
        });
}