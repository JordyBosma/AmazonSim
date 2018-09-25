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
        super()

        this.init();
    }

    init() {
        var SefRef = this;

        loadOBJModel("models/", "Export_Rocket.obj", "textures/Materials/", "Export_Rocket.mtl", (mesh) => {
            mesh.scale.set(1, 1, 1);
            SefRef.add(mesh);
        });
    }
}

class Sun extends THREE.Group {

    constructor() {
        super()

        this.init();
    }

    init() {
        var SefRef = this;

        loadOBJModel("models/", "Sun.obj", "textures/Materials/", "Sun.mtl", (mesh) => {
            mesh.scale.set(1, 1, 1);
            SefRef.add(mesh);
        });
    }
}

class Earth extends THREE.Group {

    constructor() {
        super()

        this.init();
    }

    init() {
        var SefRef = this;

        loadOBJModel("models/", "Earthchan.obj", "textures/Materials/", "Earthchan.mtl", (mesh) => {
            mesh.scale.set(1, 1, 1);
            SefRef.add(mesh);
        });
    }
}


/**
 * Load an OBJ model from the server
 * @param {string} modelPath The path to the model on the server
 * @param {string} modelName The name of the model inside the path (OBJ file)
 * @param {string} texturePath The path to the texture of the model
 * @param {string} textureName The name of the texture of the mdoel (MTL File)
 * @param {function(THREE.Mesh): void} onload The function to be called once the model is loaded and available
 * @return {void}
*/
function loadOBJModel(modelPath, modelName, texturePath, textureName, onload) {
    new THREE.MTLLoader()
        .setPath(texturePath)
        .load(textureName, function (materials) {

            materials.preload();

            new THREE.OBJLoader()
                .setPath(modelPath)
                .setMaterials(materials)
                .load(modelName, function (object) {
                    onload(object);
                }, function () { }, function (e) { console.log("Error loading model"); console.log(e) });
        });
}