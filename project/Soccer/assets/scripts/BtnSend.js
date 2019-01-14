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
        button: cc.Button,
        lable_xhq: {
            default : null,
            type: cc.Label
        }        
    },

    onLoad: function () {
        this.button.node.on('click', this.callback, this);
     },
 
    
    callback: function (t) {
        var self = this;
        var xhr = new XMLHttpRequest();        
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && (xhr.status >= 200 && xhr.status < 400)) {
                var response = xhr.responseText;
                
                if (self.lable_xhq) {
                    self.lable_xhq.string = response;
                }
                else
                {
                    console.log(response);
                }
            }
        };
        let url = "http://localhost:33333/api/todo";        
        
        xhr.open("POST", url, true);
        xhr.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        
        
        xhr.send(JSON.stringify({name:"luoyuxin",isComplete:true }));
        console.log("response send");
     }
});
