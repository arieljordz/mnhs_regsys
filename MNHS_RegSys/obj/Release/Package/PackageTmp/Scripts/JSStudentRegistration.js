$(document).ready(function () {

    var GlobalStudentID = 0;
    var GlobalAge = 0;
    var TrackID = $("#TrackID").val();
    $("#btnSaveStudent").on('click', function (e) {
        e.preventDefault();
        if (fnValidateForm("#frmStudent") == true) {
            if (GlobalStudentID == 0) {
                fnSaveStudent();
            }
            else {
                fnUpdateStudent(GlobalStudentID);
            }
        } else {
            MessageBox("Please fill the required fields.", "Information", "btn btn-danger", "Close", "bg-primarys");
        }

    });

    function fnSaveStudent() {
        $('#frmStudent input[type=text]').val(function () {
            return this.value.toUpperCase();
        });
        var frmStudent = $("#frmStudent");
        $.ajax({
            url: "/StudentRegistration/fnSaveStudent",
            data: frmStudent.serialize() + "&Age=" + GlobalAge,
            type: "POST",
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Saved Student!", "Information", "btn btn-danger", "Ok", "bg-primarys");
                    $("#frmStudent").trigger("reset");
                    fnLoadStudentTable();
                    EnabledDisabledDropDown(TrackID)
                }
                else {
                    alert("Error while saving User!");
                }
            }
        });
    }

    $("#btnUpdateStudent").on('click', function (e) {
        e.preventDefault();
        fnUpdateStudent(GlobalStudentID);
    });

    function fnUpdateStudent(GlobalStudentID) {
        $('#frmStudent input[type=text]').val(function () {
            return this.value.toUpperCase();
        });
        var frmStudent = $("#frmStudent");
        $.ajax({
            url: "/StudentRegistration/fnUpdateStudent",
            data: frmStudent.serialize() + "&StudentID=" + GlobalStudentID,
            type: "POST",
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Update Student!", "Information", "btn btn-danger", "Ok", "bg-primarys");
                    $("#frmStudent").trigger("reset");
                    $("#btnUpdateStudent").addClass("hide");
                    $("#btnSaveStudent").removeClass("hide");
                    fnLoadStudentTable();
                    EnabledDisabledDropDown(TrackID)
                }
                else {
                    alert("Error while saving User!");
                }
            }
        });
    }

    fnLoadStudentTable();
    function fnLoadStudentTable() {
        var StudentsTable = $('#StudentsTable').DataTable({
            responsive: true,
            processing: true,
            destroy: true,
            stateSave: true,
            searching: true, paging: true, info: true,
            lengthChange: true,
            order: [[1, "asc"], [2, "asc"]],
            lengthMenu: [[5, 10, 20, -1], [5, 10, 20, "All"]],
            ajax: { "url": "/StudentRegistration/fnLoadStudentTable" },
            columns:
                [
                    { data: "StudentID", title: "", visible: false, searchable: false },
                    { data: "LRN", title: "LRN", sClass: "dt-body-left", orderable: false },
                    { data: "CompleteName", title: "Complete Name", sClass: "dt-body-left", orderable: false },
                    { data: "CompleteAddress", title: "Complete Address", sClass: "dt-body-left", orderable: false },
                    //{ data: "GradeLevel", title: "Grade Level", sClass: "dt-body-center", orderable: false },
                    { data: "Track", title: "Track", sClass: "dt-body-left", orderable: false },
                    { data: "Strand", title: "Strand", sClass: "dt-body-left", orderable: false },
                    { data: "AcadStatus", title: "Academic Status", sClass: "dt-body-left", orderable: false },
                    { data: "Specialization", title: "Specialization", sClass: "dt-body-left", orderable: false },
                    { data: "SchoolYear", title: "School Year", sClass: "dt-body-left", orderable: false },
                    {
                        "data": "StudentID", title: "ACTION", "autoWidth": true, "render": function (data) {
                            return "<div class='d-flex justify-content-center'><div style='text-align:center' class='ml-3'><button type='button' class='tbl_btnEdit btn btn-primary btn-sm ml-3/*fa fa-trash*/'>Update</button></div><div style='text-align:center' class='ml-3'><button type='button' class='tbl_btnDelete btn btn-danger btn-sm ml-3/*fa fa-trash*/'>Delete</button></div></div>"
                        }
                    },
                ]
        });

        $('#StudentsTable tbody').off().on('click', 'tr', function (e) {
            e.preventDefault();
            //GlobalStudentID = StudentsTable.row(this).data().StudentID;
            if (!$(e.target).is(".tbl_btnDelete") && !$(e.target).is(".tbl_btnEdit")) {
                if (!$(this).hasClass("dtactive")) {
                    $(this).parent().find("tr").removeClass("dtactive");
                    $(this).addClass("dtactive");
                    GlobalStudentID = fnGetRowData("StudentsTable", "StudentID");
                }
                else {
                    $(this).removeClass("dtactive");
                    GlobalStudentID = 0;
                    $("#frmStudent").trigger("reset");
                    $("#btnUpdateStudent").addClass("hide");
                    $("#btnSaveStudent").removeClass("hide");
                }
            }
        }).click();
    }

    $(document).on('click', '.tbl_btnDelete', function () {
        if (GlobalStudentID != 0) {
            MessageBoxYesNo("Are you sure you want to delete this record?", "Warning", "btn btn-primarys", "btn btn-danger", "Yes", "No");
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    });

    $("#btnYes").on('click', function () {
        fnDeleteStudent(GlobalStudentID);
    })

    function fnDeleteStudent(GlobalStudentID) {
        //var GlobalStudentID = fnGetRowData("StudentsTable", "StudentID");
        $.ajax({
            url: "/StudentRegistration/fnDeleteStudent",
            type: "POST",
            data: "&StudentID=" + GlobalStudentID,
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Deleted Student!", "Information", "btn btn-danger", "Close", "bg-primarys");
                    $("#frmStudent").trigger("reset");
                    $("#btnUpdateStudent").addClass("hide");
                    $("#btnSaveStudent").removeClass("hide");
                    fnLoadStudentTable();
                }
            }
        });
    }

    $(document).on('click', '.tbl_btnEdit', function () {
        fnRetrieveStudent();
        $("#btnSaveStudent").addClass("hide");
        $("#btnUpdateStudent").removeClass("hide");
    });

    function fnRetrieveStudent() {
        //var StudentID = fnGetRowData("StudentsTable", "StudentID");
        if (GlobalStudentID != 0) {
            $.ajax({
                url: "/StudentRegistration/fnRetrieveStudent",
                type: "POST",
                data: "&StudentID=" + GlobalStudentID,
                success: function (result) {
                    if (result.success) {
                        $("[name=LRN]").val(result.LRN);
                        $("[name=LastName]").val(result.LastName);
                        $("[name=FirstName]").val(result.FirstName);
                        $("[name=MiddleName]").val(result.MiddleName);
                        $("[name=SuffixID]").val(result.SuffixID);
                        $("[name=BirthDate]").val(getStringFormatDateForDateField(result.BirthDate));
                        $("[name=Age]").val(result.Age);
                        $("[name=GenderID]").val(result.GenderID);
                        $("[name=FathersName]").val(result.FathersName);
                        $("[name=MothersName]").val(result.MothersName);
                        $("[name=Guardian]").val(result.Guardian);
                        $("[name=MobileNo]").val(result.MobileNo);
                        $("[name=RelationID]").val(result.RelationID);
                        $("[name=Street]").val(result.Street);
                        $("[name=Barangay]").val(result.Barangay);
                        $("[name=Mun_City]").val(result.Mun_City);
                        $("[name=Province]").val(result.Province);
                        $("[name=SpecID]").val(result.SpecID);
                        $("[name=GradeLevelID]").val(result.GradeLevelID);
                        $("[name=SY_ID]").val(result.SY_ID);
                        $("[name=ReligionID]").val(result.ReligionID);
                        $("[name=EthnicityID]").val(result.EthnicityID);
                        var IsCityAssistant = result.IsCityAssistance;
                        if (IsCityAssistant == true) { CheckUnchecAttr("#IschkCityAssistant", "checked", true); }
                        else { CheckUnchecAttr("#IschkCityAssistant", "checked", false); }
                        var Is4Ps = result.Is4Ps;
                        if (Is4Ps == true) { CheckUnchecAttr("#Ischk4Ps", "checked", true); }
                        else { CheckUnchecAttr("#Ischk4Ps", "checked", false); }
                        $("[name=TrackID]").val(result.TrackID);
                        $("[name=StrandID]").val(result.StrandID);
                        $("[name=AcadStatusID]").val(result.AcadStatusID);
                        var IsProvisionary = result.IsProvisionary;
                        if (IsProvisionary == true) { CheckUnchecAttr("#IschkProvisionary", "checked", true); }
                        else { CheckUnchecAttr("#IschkProvisionary", "checked", false); }
                        $("[name=PrevSchoolID]").val(result.PrevSchoolID);
                        $("[name=PrevSchoolName]").val(result.PrevSchoolName);
                        $("[name=PrevSchoolAddress]").val(result.PrevSchoolAddress);
                        $("[name=PrevSYGraduatedID]").val(result.PrevSYGraduatedID);

                        EnabledDisabledDropDown(result.TrackID);

                    }
                }
            });
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    }

    EnabledDisabledDropDown(TrackID)
    $("#TrackID").change(function () {
        TrackID = $(this).val();
        EnabledDisabledDropDown(TrackID);
    });

    $("#SpecID").attr("disabled", true);
    function EnabledDisabledDropDown(TrackID) {
        if (TrackID == 3 || TrackID == 4) {
            $("#StrandID").val("");
            $("#SpecID").val("");
            $("#StrandID").attr("disabled", true);
            $("#SpecID").attr("disabled", true);
        }
        else if (TrackID == 1) {
            $("#StrandID").removeAttr("disabled");
            $("#SpecID").val("");
            $("#SpecID").attr("disabled", true);
        }
        else if (TrackID == 2) {
            $("#StrandID").attr("disabled", true);
            $("#SpecID").val("");
            $("#SpecID").removeAttr("disabled");
        }
    }

    $("#BirthDate").change(function () {
        var dateString = $(this).val();
        getAge(dateString);
    });

    function getAge(dateString) {
        var Bdate = document.getElementById('BirthDate').value;
        var Bday = +new Date(Bdate);
        var RawAge = ((Date.now() - Bday) / (31557600000));
        var Age = Math.trunc(RawAge);
        $("#Age").val(Age);
        GlobalAge = Age;
    }

    ///////////////////////////////////////////////
    //function fnValidateForm(formIDs) {
    //    if (!$(formIDs).parsley().validate()) {
    //        return false;
    //    }
    //    else {
    //        return true;
    //    }
    //}
});                                                   //end of document

