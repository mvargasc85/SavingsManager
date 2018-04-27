// this script implements a facade pattern in order to initialize the components of group, socio, plan, ahorro views

var savingsfacade = null;
var savingsObservable = null;



$(document).ready(function () {
    $("#Menu1").kendoMenu();
    savingsfacade = new SavingsFacade();
    savingsfacade.initializeGroup();
    savingsfacade.initializeSocio();
    savingsfacade.initializePlan();
    savingsfacade.initializeAhorro();

});

function display(e) { alert(e); }


var SavingsFacade = function () {
    if (typeof Group != undefined)
        this.group = new Group();
    if (typeof Socio != undefined)
        this.socio = new Socio();
    if (typeof Plan != undefined)
        this.plan = new Plan();
    if (typeof Ahorro != undefined)
        this.ahorro = new Ahorro();
}

SavingsFacade.prototype = {
    initializeGroup: function () {
        this.group.initializeBtns();
        this.group.loadGroupGrid();
    },

    initializeSocio: function () {
        this.socio.initializeBtns();
        this.socio.loadSociosGrid();
        this.group.loadGroupDropDownList();
    },

    initializePlan: function () {
        this.plan.initializeBtns();
        this.plan.loadPlansGrid();
        this.group.loadGroupDropDownList();
    },

    initializeAhorro: function () {
        this.ahorro.initializeBtns();
        this.ahorro.loadAhorrosGrid();        
        this.socio.loadSocioDropDownList();
        this.plan.loadPlanDropDownList();
    }
}


var SavingsObservable  = function () {
    this.listeners = [];  // observers
}

SavingsObservable.prototype = {

    subscribe: function (fn) {
        this.listeners.push(fn);
    },

    unsubscribe: function (fn) {
        this.listeners = this.listeners.filter(
            function (item) {
                if (item !== fn) {
                    return item;
                }
            }
        );
    },

    fire: function (sender, msg) {
        this.listeners.forEach(function (item) {
            item(sender,msg);
        });
    }
}


var notifySuccess = function (sender, msg) {
    var text = '';
    if (msg == 'SOk')
        text = sender + ' creado exitosamente';
    else if (msg == 'UOk')
        text = sender + ' actualizado exitosamente';
    else if (msg == 'DOk')
        text = sender + ' elimando exitosamente';
    else
        return;

    $("#notificationDiv").html(text);
    $("#notificationDiv").show();
    $("#notificationDiv").addClass('successNotification notify-animation');
}


var notifyError = function (sender, msg) {
    var text = '';
    if (msg == 'SF')
        text = 'Ha ocurrido un error al crear el ' + sender;
    else if (msg == 'UF')
        text = 'Ha ocurrido un error al actualizar el ' + sender;
    else if (msg == 'DF')
        text = 'Ha ocurrido un error al eliminar el ' + sender;
    else
        return;

    $("#notificationDiv").html(text);
   
    $("#notificationDiv").show();
    $("#notificationDiv").addClass('errorNotification notify-animation');
}


GetSavingsObserver = function () {
    //if (savingsObservable == null || savingsObservable == undefined) {
        savingsObservable = new SavingsObservable();
        savingsObservable.subscribe(notifySuccess);
        savingsObservable.subscribe(notifyError);
    //}
    return savingsObservable;
}