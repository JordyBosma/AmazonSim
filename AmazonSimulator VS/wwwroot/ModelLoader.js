class Robot extends THREE.Group {

    constructor() {
        super();
        
        //this._Loadstate = LoadStates.NOT_LOADING;
        this.init();
    }

    init() {
        //if (this._Loadstate != LoadStates.NOT_LOADING) {
        //    return;
        //}

        //this._Loadstate = LoadStates.LOADING;

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

        //this._Loadstate = LoadStates.LOADED;
    }
}