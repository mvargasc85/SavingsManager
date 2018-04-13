﻿$(document).ready(function () {
   
    $("#AddAhorroBtn").on("click", saveAhorro);
    $("#cancelAddingAhorroBtn").on("click", cancelAddingAhorro);
    getAllAhorros();
    getSocioDropDownList();
    getPlanDropDownList();

   
});



function saveAhorro() {

    var ahorroModel = getAhorroModel();
    $.ajax({
        type: "Post",
        url: "/Ahorro/CrearAhorro",
        data: ahorroModel,
        success: function (result) {
            var url = "/Ahorro/VerAhorros";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
}


function getAhorroModel() {
    var ahorroModel =
        {
            Idpago: null,
            IdPlan: dropDownListObject("planDropDown").value(),
            IdSocio: dropDownListObject("socioDropDown").value(),
            Fecha: $("#fechaTxt").val(),
            MontoCuota: $("#montoCuotaTxt").val(),
            Estado: dropDownListObject("socioDropDown").value()
        };

    return ahorroModel;
}


function cancelAddingAhorro() {
    $("#fechaTxt").val("");
    $("#montoCuotaTxt").val("");
}

function getPlanDropDownList() {
    debugger;
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


function getSocioDropDownList() {
    debugger;
    var socioId = $("#socioDropDown").val();
    $.ajax({
        url: "/Socio/GetSocios",
        type: "get",
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            CreateDropDownlist("socioDropDown", $.parseJSON(result), "Nombre", "IdSocio", null, "Seleccione ...", null);

        },
        error: function () { alert("Problema al cargar los socios") }
    });
}



function showSociosCombo(result) {

    if (result !== "") {
        createSociosGrid("sociosGrid", result);
        $("#sociosDiv").show();
    } else
        $("#sociosDiv").hide();

}

function getAllAhorros() {
    $.ajax({
        type: "get",
        url: "/Ahorro/GetAhorros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //  async: true,
        success: function (result) {
            showAhorrosGrid(result);
        },
        error: function (e) {
            display(e);
        }
    });
}


function showAhorrosGrid(result) {

    if (result !== "") {
        createAhorrosGrid("ahorrosGrid", result);
        $("#ahorrosDiv").show();
        //var url = "/Home/VerGrupos";
        //window.location.href = url;
    } else
        $("#ahorrosDiv").hide();

}

function createAhorrosGrid(divId, items) {
    $("#" + divId).kendoGrid({
        dataSource: {
            data: items,
            schema: {
                model: {
                    fields: {
                        idpago: { type: "number" },
                        IdPlan: { type: "number" },
                        IdSocio: { type: "number" },
                        Fecha: { type: "date" },
                        MontoCuota: { type: "decimal" },
                        Estado: { type: "char" }
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
            { field: "IdPlan", title: "Plan", width: "20px" },
            { field: "IdSocio", title: "Socio", width: "20px" },
            { field: "Fecha", title: "Fecha", width: "20px" },
            { field: "MontoCuota", title: "Monto Cuota", width: "30px" },
            { field: "Estado", title: "Estado", width: "10px" },
            {
                template: '<a href="javascript:void(0)" class="k-grid-edit" onclick="EditAhorro(${idpago})">Editar</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;' +
                '<a href="javascript:void(0)" class="k-grid-delete" onclick="ConfirmDeleteAhorro(${idpago})">Eliminar</a>',
                width: "40px", attributes: { style: "text-align:center;" }
            }
        ]
    });
}

function EditAhorro(idpago) {
    var url = "/Ahorro/EditarAhorro?idpago=" + idpago;
    window.location.href = url;
}
function ConfirmDeleteAhorro(idpago) {
    $("#delete-ahorro-dialog-confirm").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Eliminar": function () {
                deleteAhorro(idpago);
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });
}

function deleteAhorro(idpago) {
    $.ajax({
        type: "Post",
        url: "/Ahorro/EliminarAhorro?idpago=" + idpago,
        success: function (result) {
            var url = "/Ahorro/VerAhorros";
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