$(document).ready(function () {
    $("#Menu1").kendoMenu();
    $("#AddPlanBtn").on("click", savePlan);
    $("#cancelAddingPlanBtn").on("click", cancelAddingPlan);
    getGroupDropDownList();
});

function loadPlanInfo() {
    //$.ajax({
    //    type: "get",
    //    url: "/Home/GetGroupById?idGrupo=" + idGrupo,
    //    success: function (result) {
    //        var url = "/Home/EditarGrupo";
    //        window.location.href = url;
    //    },
    //    error: function (e) { display(e); }
    //});
}

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

