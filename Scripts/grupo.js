var savingsObservable = null;
$(document).ready(function () {

    savingsObservable = GetSavingsObserver();

    if ($("#savingsAction") && $("#savingsAction").val() != undefined && $("#savingsAction").val() != '') {
        var SavingsAction = $("#savingsAction").val();
        savingsObservable.fire('Grupo', SavingsAction);
    }

});

//groups methods
var saveGroup = function () {

    var groupModel = getGroupModel();
    $.ajax({
        type: "Post",
        url: "/Group/CrearGrupo",
        data: groupModel,
        success: function (result) {
           
            var url = "/Group/VerGrupos";
            window.location.href = url;
            
        },
        error: function (e) { display(e); }
    });
}

function getGroupModel() {
    var groupModel =
    {
        IdGrupo: null,
        Nombre: $("#groupNameTxt").val(),
        Descripcion: $("#groupDescriptionTxt").val()
    };

    return groupModel;
}

var cancelAddingGroup = function () {
    $("#groupNameTxt").val("");
    $("#groupDescriptionTxt").val("");
}

var getAllGroups = function () {
    $.ajax({
        type: "get",
        url: "/Group/GetGrupos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //  async: true,
        success: function (result) {
            showGroupsGrid(result);
        },
        error: function (e) {
            display(e);
        }
    });
}

function showGroupsGrid(result) {

    if (result !== "") {
        createGroupsGrid("groupsGrid", result);
        $("#groupsDiv").show();
    } else
        $("#groupsDiv").hide();

}

function createGroupsGrid(divId, items) {
    $("#" + divId).kendoGrid({
        dataSource: {
            data: items,
            schema: {
                model: {
                    fields: {
                        IdGrupo: { type: "number" },
                        Nombre: { type: "string" },
                        Descripcion: { type: "string" },
                        FechaCreacion: { type: "date" }
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
            { field: "IdGrupo", title: "Id", width: "10px" },
            { field: "Nombre", title: "Nombre" },
            { field: "Descripcion", title: "Descripcion" },
            { field: "FechaCreacion", title: "Fecha de Creación", width: "30px", format: "{0: dd/MM/yyyy}" },
            {
                template: '<a href="javascript:void(0)" class="k-grid-edit" onclick="EditGroup(${IdGrupo})">Editar</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;' +
                    '<a href="javascript:void(0)" class="k-grid-delete" onclick="ConfirmDeleteGroup(${IdGrupo})">Eliminar</a>',
                width: "40px", attributes: { style: "text-align:center;" }
            }

        ]
    });
}

function EditGroup(idGrupo) {
    var url = "/Group/EditarGrupo?idGrupo=" + idGrupo;
    window.location.href = url;
}

function ConfirmDeleteGroup(idGrupo) {
    $("#delete-group-dialog-confirm").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Eliminar": function () {
                deleteGroup(idGrupo);
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });
}

function deleteGroup(idGrupo) {
    $.ajax({
        type: "Post",
        url: "/Group/EliminarGrupo?idGrupo=" + idGrupo,
        success: function (result) {
            var url = "/Group/VerGrupos";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
}

function updateGroup() {

    var groupModel = getGroupModel();
    $.ajax({
        type: "Post",
        url: "/Group/CrearGrupo",
        data: groupModel,
        success: function (result) {
            var url = "/Group/VerGrupos";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
}

var getGroupDropDownList = function () {
      var grupoId = $("#grupoDropDown").val();
    $.ajax({
        url: "/Group/GetGrupos",
        type: "get",
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            CreateDropDownlist("grupoDropDown", $.parseJSON(result), "Nombre", "IdGrupo", null, "Seleccione ...", null);

        },
        error: function () { alert("Problema al cargar los grupos") }
    });
}

var Group = function () { }

Group.prototype = {
    initializeBtns: function () {
        $("#AddGroupBtn").on("click", saveGroup);
        $("#cancelAddingGroupBtn").on("click", cancelAddingGroup);
    },

    loadGroupGrid: function () {
        getAllGroups();
    },

    loadGroupDropDownList: function () {
        getGroupDropDownList();
    }
}
