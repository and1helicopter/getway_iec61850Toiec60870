class server61850 {
    constructor(){
        this.host = {};
        this.port = {};
        this.apTitleR = '1.1.1.999.1';
        this.aeQualifierR = '12';
        this.pSelectorR = '1';
        this.sSelectorR = '1';
        this.tSelectorR = '0';
        this.apTitleL = '1.1.1.999';
        this.aeQualifierL = '12';
        this.pSelectorL = '1';
        this.sSelectorL = '1';
        this.tSelectorL = '0';
        this.enabled = false;
        this.password = '';
        this.items61850 = [];
        this.itemsASDU = [];
    }
}

class ASDU{
    constructor(){
        this.typeID = {};
        this.vsq = {};
        this.cot = {};
        this.isNegative = {};
        this.isTest = {};
        this.oa = {};
        this.ca = {};
        this.objects = [];
    }
}

class Object61850{
    constructor(path, typeFC, typeMMS) {
        this.path = path;
        this.typeFC = typeFC;
        this.typeMMS = typeMMS;
    }
}

data = {
    host:'localhost',
    port: 2404,
    maxQueue: 10,
    maxConnection: 1,
    statusTls: false,
    servers61850:[]
};

actual = {
    index: 0,
    add_item61850: []
};

class info_add_item61850{
    constructor(){
        this.path ='';
        this.typeFC = '-1';
        this.typeMMS = '0';
        this.show = false;
    }
}

show = {
    show61850: [],
    show60870: false
};

start = {
    run: []
};

map = {
    data: data,
    actual: actual,
    show: show,
    start: start
};

Vue.component('app-iec61850',{
    template: '#iec61850',
    data: function () {
        return map
    },
    methods: {
        startServer: function () {
            map.start.run[map.actual.index] = !map.start.run[map.actual.index];
            map.data.servers61850.sort();
            //Запуск или остановка сервера
        },
        removeServer: function () {

            map.start.run[map.actual.index] = false;
            map.start.run.splice(map.actual.index, 1);
            map.show.show61850[map.actual.index] = false;
            this.$emit('showstatuschange', map.show.show61850[map.actual.index]);
            map.show.show61850.splice(map.actual.index, 1);
            map.data.servers61850.splice(map.actual.index, 1);
            map.actual.add_item61850.splice(map.actual.index, 1);
            map.data.servers61850.sort();
            //Запуск или остановка сервера
        },
        add_show_new_item: function () {
            actual.add_item61850[actual.index].show = true;
        },
        add_new_item: function () {

            actual.add_item61850[actual.index].show = false;

            data.servers61850[actual.index].items61850.push(new Object61850(actual.add_item61850[actual.index].path, actual.add_item61850[actual.index].typeFC ,actual.add_item61850[actual.index].typeMMS));
        }
    }
});

Vue.component('app-header_left', {
    template: '#headerLeft',
    data: function () {
        return map
    },
    methods: {
        add_server61850: function () {
            map.data.servers61850.push(new server61850());
            map.data.servers61850[data.servers61850.length - 1].host = "127.0.0.1";
            map.data.servers61850[data.servers61850.length - 1].port = 102;
            map.start.run.push(false);
            map.actual.add_item61850.push(new info_add_item61850());

            map.show.show61850.push(false);

            console.log(show.show61850[show.show61850.length - 1]);
        },
        show_server61850: function (server, index) {
            map.show.show61850[index] = !show.show61850[index];
            for(let i = 0; i < map.show.show61850.length; i++){
                if(i !== index){map.show.show61850[i] = false;}
            }
            this.$emit('showstatuschange', map.show.show61850[index]);
            map.actual.index = index;
            map.data.servers61850.sort();

            console.log(index);
            console.log(server);
        }
    }
});

Vue.component('app-iec60870', {
    template: '#iec60870',
    data: function () {
        return map
    },
    methods: {
        changeHost: function () {
            console.log(map.data.host)
        },
        changePort: function () {
            console.log(map.data.port)
        }
    }
});

Vue.component('app-header_right', {
    template: '#headerRight',
    data: function () {
        return map
    },
    methods: {
        show_on: function () {
            map.show.show60870 = !map.show.show60870;
            this.$emit('showstatuschange',  map.show.show60870);
            map.data.servers61850.sort();

            console.log(map.show.show60870);
        }
    }
});

new Vue({
    el: '#app',
    data:{
        show60870: false,
        show61850: false
    },
    methods:{
        change_show60870: function (value) {
            this.show60870 = value;
            if(this.show60870){
                this.show61850 = false;
                for(let i = 0; i < show.show61850.length; i++){
                    show.show61850[i] = false;
                }
            }
            console.log(this.show60870);
        },
        change_show61850: function (value) {
            this.show61850 = value;
            if(this.show61850){
                this.show60870 = false
            }
            console.log(this.show61850);
        }
    }
});