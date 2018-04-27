// this script implements a facade pattern in order to initialize the components of group, socio, plan, ahorro views

var savingsfacade = null;

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