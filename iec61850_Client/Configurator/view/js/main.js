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
        this.typeFC = 'NONE';
        this.typeMMS = 'MMS_ARRAY';
        this.show = false;
        this.edit_item_iec61850 = [];
    }
}

show = {
    show61850: [],
    show60870: false
};

start = {
    run: []
};

options_iec61850 = {
    options_fc: [
        { text: 'NONE', value: 'NONE'},
        { text: 'ST', value: 'ST'},
        { text: 'MX', value: 'MX'},
        { text: 'SP', value: 'SP'},
        { text: 'SV', value: 'SV'},
        { text: 'CF', value: 'CF'},
        { text: 'DC', value: 'DC'},
        { text: 'SG', value: 'SG'},
        { text: 'SE', value: 'SE'},
        { text: 'SR', value: 'SR'},
        { text: 'OR', value: 'OR'},
        { text: 'BL', value: 'BL'},
        { text: 'EX', value: 'EX'},
        { text: 'CO', value: 'CO'},
        { text: 'US', value: 'US'},
        { text: 'MS', value: 'MS'},
        { text: 'RP', value: 'RP'},
        { text: 'BR', value: 'BR'},
        { text: 'LG', value: 'LG'},
        { text: 'ALL', value: 'ALL'},
    ],
    options_mms: [
        { text: 'MMS_ARRAY', value: 'MMS_ARRAY'},
        { text: 'MMS_STRUCTURE', value: 'MMS_STRUCTURE'},
        { text: 'MMS_BOOLEAN', value: 'MMS_BOOLEAN'},
        { text: 'MMS_BIT_STRING', value: 'MMS_BIT_STRING'},
        { text: 'MMS_INTEGER', value: 'MMS_INTEGER'},
        { text: 'MMS_UNSIGNED', value: 'MMS_UNSIGNED'},
        { text: 'MMS_FLOAT', value: 'MMS_FLOAT'},
        { text: 'MMS_OCTET_STRING', value: 'MMS_OCTET_STRING'},
        { text: 'MMS_VISIBLE_STRING', value: 'MMS_VISIBLE_STRING'},
        { text: 'MMS_GENERALIZED_TIME', value: 'MMS_GENERALIZED_TIME'},
        { text: 'MMS_BINARY_TIME', value: 'MMS_BINARY_TIME'},
        { text: 'MMS_BCD', value: 'MMS_BCD'},
        { text: 'MMS_OBJ_ID', value: 'MMS_OBJ_ID'},
        { text: 'MMS_STRING', value: 'MMS_STRING'},
        { text: 'MMS_UTC_TIME', value: 'MMS_UTC_TIME'},
        { text: 'MMS_DATA_ACCESS_ERROR', value: 'MMS_DATA_ACCESS_ERROR'}
    ]
};

map = {
    data: data,
    actual: actual,
    show: show,
    start: start,
    options_iec61850: options_iec61850
};

function test(list, status, index) {
    alert("test_injecting");
    var list0 = JSON.parse(list);
    map.data.servers61850[index].items61850 = [];
    for(var i=0; i < list0.length; i++)
    {
        console.log(list0[i]);
        map.data.servers61850[index].items61850.push(new Object61850(list0[i].path, list0[i].typeFC, list0[i].typeMMS));
    }
    //map.data.servers61850.
    console.log('test_injecting');
    map.data.servers61850.sort();
    //console.log(list);
}


async function testlol() {
    map.start.run[map.actual.index] = !map.start.run[map.actual.index];
    map.data.servers61850.sort();
    //Запуск или остановка сервера
    //data.servers61850[actual.index] - передача настроек
    var output = JSON.stringify(data.servers61850[actual.index]);
    var status = start.run[actual.index];
    var index = actual.index;
    console.log(output);
    var list = await serverStart.startServer61850(output, status, index);
    test(list, status, index);
}

Vue.component('app-iec61850',{
    template: '#iec61850',
    data: function () {
        return map
    },
    methods: {
        startServer: function () {
            testlol();
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
            actual.add_item61850[actual.index].edit_item_iec61850.push(false);
            data.servers61850[actual.index].items61850.push(new Object61850(actual.add_item61850[actual.index].path, actual.add_item61850[actual.index].typeFC ,actual.add_item61850[actual.index].typeMMS));
        },
        close_mew_item: function () {
            actual.add_item61850[actual.index].show = false;
        },
        edit_item_iec61850: function (index) {
            actual.add_item61850[actual.index].edit_item_iec61850[index] = ! actual.add_item61850[actual.index].edit_item_iec61850[index];
            data.servers61850[actual.index].items61850.sort();
            console.log(actual.add_item61850[actual.index].edit_item_iec61850[index]);
        },
        remove_item_iec61850: function(index){
            data.servers61850[actual.index].items61850.splice(index, 1);
            actual.add_item61850[actual.index].edit_item_iec61850.splice(index, 1);
            data.servers61850[actual.index].items61850.sort();
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
            //Добавить новый сервер
            var output = JSON.stringify(data.servers61850[data.servers61850.length - 1]);
            serverStart.addServer61850(output);
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