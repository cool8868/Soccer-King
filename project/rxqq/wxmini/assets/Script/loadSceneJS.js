
cc.Class({
    extends: cc.Component,

    properties: {
        button: cc.Button,
        scene: ''
    },

    // LIFE-CYCLE CALLBACKS:

    onLoad () {
        this.button.node.on('click', this.callback, this);
    },

    callback: function (button) {
        cc.director.loadScene(this.scene);
     }
});
