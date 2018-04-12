$(document).ready(function () {
   
    $("#AddPlanBtn").on("click", savePlan);
    $("#cancelAddingPlanBtn").on("click", cancelAddingPlan);
    getAllPlanes();
    getGroupDropDownList();

   
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
                        FechaInicial: { type: "datetime" },
                        FechaFinal: { type: "datetime" },
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
            { field: "Nombre", title: "Nombre", width: "50px" },
            { field: "Descripcion", title: "Descripcion", width: "50px" },
            { field: "Duracion", title: "DuracionDuracion", width: "50px" },
            { field: "Periodicidad", title: "Periodicidad", width: "50px" },
            { field: "MontoCuota", title: "MontoCuota", width: "50px" },
            { field: "FechaInicial", title: "FechaInicial", width: "50px" },
            { field: "FechaFinal", title: "FechaFinal", width: "50px" },
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


function CreateDropDownlist(divId, items, text, value, onchanceEventHandler, selectPlaceHolder, dataBoundEvent) {

    $("#" + divId).kendoDropDownList({
        optionLabel: selectPlaceHolder,
        dataTextField: text,
        dataValueField: value,
        dataSource: items,
        index: 0,
        change: onchanceEventHandler,
        open: dataBoundEvent
    });

    dropDownListObject(divId).select(0);
}


var dropDownListObject = (function (ddlId) {
    return $('#' + ddlId).data("kendoDropDownList");
});