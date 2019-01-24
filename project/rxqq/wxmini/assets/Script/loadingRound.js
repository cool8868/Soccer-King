cc.Class({
    extends: cc.Component,

    properties: {
       
    },

    setRotateAction: function () {        
        var rotateAction = cc.rotateBy(1, 360).easing(cc.easeCircleActionInOut());        
        // 不断重复
        return cc.repeatForever(rotateAction);
    },
    
    // LIFE-CYCLE CALLBACKS:

    onLoad () {
        // 初始化跳跃动作
        this.rotateAction = this.setRotateAction();
        this.node.runAction(this.rotateAction);
    } 
    
});
