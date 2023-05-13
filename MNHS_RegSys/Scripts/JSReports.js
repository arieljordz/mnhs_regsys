$(document).ready(function () {

    var Type = $("#ReportTypeID").val();
    //GenerateReport();
    //function GenerateReport() {
    //    $("#btnGenerateReport").on("click", function () {
    //        var rptUrl = "../Reports/ReportFromDashboard";
    //        $("#GenerateReport").attr('src', rptUrl);
    //    });
    //}

    jQuery(document).bind("keyup keydown", function (e) {
        if (e.ctrlKey && e.keyCode == 80) {
            return false;
            //var el = "GenerateReport";
            //printContent(el);
        }
    });

    $("#btnGenerateReport").on("click", function () {
        var el = "";
        if (Type == 1) {
            el = "Dashboards";
            printContent(el);
            $("#ReportTypeID").val(1);
        }
        else if (Type == 2) {
            el = "Strand";
            printContent(el);
            $("#ReportTypeID").val(2);
        }
        else if (Type == 3) {
            el = "Track";
            printContent(el);
            $("#ReportTypeID").val(3);
        }
        else if (Type == 4) {
            el = "Specialization";
            printContent(el);
            $("#ReportTypeID").val(4);
        }
        else if (Type == 5) {
            el = "GradeLevels";
            printContent(el);
            $("#ReportTypeID").val(5);
        }
    });

    function printContent(el) {
        var restorepage = $('body').html();
        var printcontent = $('#' + el).clone();
        $('body').empty().html(printcontent);
        window.print();
        $('body').html(restorepage);
    }

    $("#ReportTypeID").change(function () {
        Type = $(this).val();
        ByType(Type);
    });

    ByType(Type);
    function ByType(Type) {
        if (Type == 1) {
            $("#dvByStrand").addClass("hide");
            $("#dvByTrack").addClass("hide");
            $("#dvBySpecialization").addClass("hide");
            $("#dvByGradeLevel").addClass("hide");
            $("#dvDashBoard").removeClass("hide");
        }
        else if (Type == 2) {
            $("#dvDashBoard").addClass("hide");
            $("#dvByTrack").addClass("hide");
            $("#dvBySpecialization").addClass("hide");
            $("#dvByGradeLevel").addClass("hide");
            $("#dvByStrand").removeClass("hide");
        }
        else if (Type == 3) {
            $("#dvByStrand").addClass("hide");
            $("#dvDashBoard").addClass("hide");
            $("#dvBySpecialization").addClass("hide");
            $("#dvByGradeLevel").addClass("hide");
            $("#dvByTrack").removeClass("hide");
        }
        else if (Type == 4) {
            $("#dvByTrack").addClass("hide");
            $("#dvByStrand").addClass("hide");
            $("#dvDashBoard").addClass("hide");
            $("#dvByGradeLevel").addClass("hide");
            $("#dvBySpecialization").removeClass("hide");
        }
        else if (Type == 5) {
            $("#dvByTrack").addClass("hide");
            $("#dvByStrand").addClass("hide");
            $("#dvDashBoard").addClass("hide");
            $("#dvBySpecialization").addClass("hide");
            $("#dvByGradeLevel").removeClass("hide");
        }
    }
});         //end of document

