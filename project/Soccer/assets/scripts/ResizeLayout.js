// Learn cc.Class:
//  - [Chinese] http://docs.cocos.com/creator/manual/zh/scripting/class.html
//  - [English] http://www.cocos2d-x.org/docs/creator/en/scripting/class.html
// Learn Attribute:
//  - [Chinese] http://docs.cocos.com/creator/manual/zh/scripting/reference/attributes.html
//  - [English] http://www.cocos2d-x.org/docs/creator/en/scripting/reference/attributes.html
// Learn life-cycle callbacks:
//  - [Chinese] http://docs.cocos.com/creator/manual/zh/scripting/life-cycle-callbacks.html
//  - [English] http://www.cocos2d-x.org/docs/creator/en/scripting/life-cycle-callbacks.html

cc.Class({
    extends: cc.Component,

    properties: {
        LMG: {
            default: null,      
            type: cc.Layout
        }
        // bar: {
        //     get () {
        //         return this._bar;
        //     },
        //     set (value) {
        //         this._bar = value;
        //     }
        // },
    },

    // LIFE-CYCLE CALLBACKS:

    onLoad () {

    },

    start () {        
        if(!this.LMG)
        {
            console.log("Not found layoutImg");
            return;
        }
        var layoutStory = this.node;
        if(!layoutStory)
        {
            console.log("Not found layoutStory");
            return;
        }
        let pY = this.LMG.node.y - (this.LMG.node.height + layoutStory.height)/2;
        //layoutStory.height = 500;
        layoutStory.setPosition(0,pY);
    },

    // update (dt) {},
});
