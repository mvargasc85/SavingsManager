var savingsObservable = null;
$(document).ready(function () {

    savingsObservable = GetSavingsObserver();

    if ($("#savingsAction") && $("#savingsAction").val() != undefined && $("#savingsAction").val() != '') {
        var SavingsAction = $("#savingsAction").val();
        savingsObservable.fire('Plan', SavingsAction);
    }

});


function savePlan() {

    var planModel = getPlanModel();
    $.ajax({
        type: "Post",
        url: "/Plan/CrearPlan",
        data: planModel,
        success: function (result) {
            var url = "/Plan/VerPlanes";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
}


function getPlanModel() {
    var planModel =
        {
            IdPlan: null,
            Nombre: $("#planNombreTxt").val(),
            Descripcion: $("#planDescripcionTxt").val(),
            Duracion: $("#planDuracionTxt").val(),
            Periodicidad: $("#planPeriodicidadTxt").val(),
            MontoCuota: $("#planMontoCuotaTxt").val(),
            FechaInicial: $("#planFechaInicialTxt").val(),
            FechaFinal: $("#planFechaFinalTxt").val(),
        };

    return planModel;
}


function cancelAddingPlan() {
    $("#planNombreTxt").val("");
    $("#planDuracionTxt").val("");
    $("#planPeriodicidadTxt").val("");
    $("#planMontoCuotaTxt").val("");
    $("#planFechaInicialTxt").val("");
    $("#planFechaFinalTxt").val("");
}


function getAllPlanes() {
    $.ajax({
        type: "get",
        url: "/Plan/GetPlanes",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //  async: true,
        success: function (result) {
            showPlanesGrid(result);
        },
        error: function (e) {
            display(e);
        }
    });
}


function showPlanesGrid(result) {

    if (result !== "") {
        createPlanesGrid("planesGrid", result);
        $("#planesDiv").show();
    } else
        $("#planesDiv").hide();

}

function createPlanesGrid(divId, items) {
    $("#" + divId).kendoGrid({
        dataSource: {
            data: items,
            schema: {
                model: {
                    fields: {
                        IdPlan: { type: "number" },
                        Descripcion: { type: "string" },
                        Duracion: { type: "number" },
                        Periodicidad: { type: "string" },
                        MontoCuota: { type: "number" },
                        FechaInicial: { type: "date" },
                        FechaFinal: { type: "date" },
                    }
                }
            },

            pageSize: 5
        },
        //        height: 100,
        scrollable: false,
        sortable: true,
        filterable: true,
        columns: [
            { field: "Nombre", title: "Nombre", width: "70px" },
            { field: "Descripcion", title: "Descripcion", width: "70px" },
            { field: "Duracion", title: "Duracion", width: "20px" },
            { field: "Periodicidad", title: "Periodicidad", width: "20px" },
            { field: "MontoCuota", title: "Monto Cuota", width: "40px" },
            { field: "FechaInicial", title: "Fecha Inicial", width: "30px", format: "{0: dd/MM/yyyy}"  },
            { field: "FechaFinal", title: "Fecha Final", width: "30px" , format: "{0: dd/MM/yyyy}" },
            {
                template: '<a href="javascript:void(0)" class="k-grid-edit" onclick="EditPlan(${IdPlan})">Editar</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;' +
                '<a href="javascript:void(0)" class="k-grid-delete" onclick="ConfirmDeletePlan(${IdPlan})">Eliminar</a>',
                width: "40px", attributes: { style: "text-align:center;" }
            }
        ]
    });
}

function EditPlan(idPlan) {
    var url = "/plan/EditarPlan?idPlan=" + idPlan;
    window.location.href = url;
}

function ConfirmDeletePlan(idPlan) {
    $("#delete-plan-dialog-confirm").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Eliminar": function () {
                deletePlan(idPlan);
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });
}

function deletePlan(idPlan) {
    $.ajax({
        type: "Post",
        url: "/Plan/EliminarPlan?idPlan=" + idPlan,
        success: function (result) {
            var url = "/Plan/VerPlanes";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
}

function display(e) { alert(e); }

function getPlanDropDownList() {
    var socioId = $("#planDropDown").val();
    $.ajax({
        url: "/Plan/GetPlanes",
        type: "get",
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            CreateDropDownlist("planDropDown", $.parseJSON(result), "Nombre", "IdPlan", null, "Seleccione ...", null);

        },
        error: function () { alert("Problema al cargar los planes") }
    });
}

var Plan = function () { }

Plan.prototype = {
    initializeBtns: function () {
        $("#AddPlanBtn").on("click", savePlan);
        $("#cancelAddingPlanBtn").on("click", cancelAddingPlan);
    },

    loadPlansGrid: function () {
        getAllPlanes();
    },

    loadPlanDropDownList: function () {
        getPlanDropDownList();
    }
}



