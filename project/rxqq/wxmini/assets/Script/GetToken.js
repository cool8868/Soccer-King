// Learn cc.Class:
//  - [Chinese] https://docs.cocos.com/creator/manual/zh/scripting/class.html
//  - [English] http://docs.cocos2d-x.org/creator/manual/en/scripting/class.html
// Learn Attribute:
//  - [Chinese] https://docs.cocos.com/creator/manual/zh/scripting/reference/attributes.html
//  - [English] http://docs.cocos2d-x.org/creator/manual/en/scripting/reference/attributes.html
// Learn life-cycle callbacks:
//  - [Chinese] https://docs.cocos.com/creator/manual/zh/scripting/life-cycle-callbacks.html
//  - [English] https://www.cocos2d-x.org/docs/creator/manual/en/scripting/life-cycle-callbacks.html

cc.Class({
    extends: cc.Component,

    properties: {
        button: cc.Button
    },

    // LIFE-CYCLE CALLBACKS:

    onLoad () {
        this.button.node.on('click', this.callback, this);
    },

    callback: function (button) {
        var xhr = new XMLHttpRequest();        
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && (xhr.status >= 200 && xhr.status < 400)) {
                cc.sys.localStorage.setItem("token", xhr.responseText);
                console.log(xhr.responseText);
            }
            
        };
        let url = "http://localhost:59480/api/token/successopenid";        
        
        xhr.open("GET", url, true);
        xhr.send();
     }
});
