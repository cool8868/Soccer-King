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
        var token = cc.sys.localStorage.getItem("token");
        if(token)
        {
            var xhr = new XMLHttpRequest();                   
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && (xhr.status >= 200 && xhr.status < 400)) {
                    //cc.sys.localStorage.setItem("token", xhr.responseText)
                    console.log(xhr.responseText);
                }
                
            };
            let url = "http://localhost:59480/api/chou/1";        
            
            xhr.open("GET", url, true);
            xhr.setRequestHeader("Authorization", 'Bearer ' + token) 
            xhr.send();
        }
     }
});
