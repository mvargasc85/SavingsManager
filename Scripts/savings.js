$(document).ready(function () {
    $("#Menu1").kendoMenu();
    //$("#saveSurveyBtn").on("click", SaveSurvey);
    $("#cancelBtn").on("click", cancelSurvey);
    GetCPSPInfo();
    createDropDownsForLocation();


});