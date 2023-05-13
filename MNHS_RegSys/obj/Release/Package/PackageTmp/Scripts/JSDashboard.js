$(document).ready(function () {

    Dashboards();
    function Dashboards() {
        var options = {
            title: {
                display: false,
                fontsize: 14,
                text: 'Total de Pedidos por Situação'
            }, legend:
                {
                    display: true,
                    position: 'right'
                }
        };


        $.ajax({
            url: '../Dashboard/GetStatistics',
            type: 'post',
            success: function (result) {

                //Total Students
                $("#lblTotalStudents").html(result.TotalStudents.Students);
                $("#lblTotalGirls").html(result.GirlsStudents.Girls);
                $("#lblTotalBoys").html(result.BoysStudents.Boys);

                //Teacher
                $("#lblTeacherStudentRatio").html(result.RatioStudentAdviser.StudentAdviserRatio);
                $("#lblTeacherRatio").html(result.TotalAdvisers.Advisers / result.TotalAdvisers.Advisers);
                $("#lblStudentTeach").html(result.TotalStudents.Students);
                $("#lblTeachers").html(result.TotalAdvisers.Advisers);

                //ClassRooms
                $("#lblClassRoomsStudentRatio").html(result.RatioStudentClassRoom.StudentClassRoomRatio);
                $("#lblClassRoomRatio").html(result.TotalClassRooms.ClassRooms / result.TotalClassRooms.ClassRooms);
                $("#lblStudentClass").html(result.TotalStudents.Students);
                $("#lblClassRooms").html(result.TotalClassRooms.ClassRooms);

                //Computers
                $("#lblComputerStudentRatio").html(result.RatioStudentComputer.StudentComputerRatio);
                $("#lblComputerRatio").html(result.TotalComputers.Computers / result.TotalComputers.Computers);
                $("#lblStudentComp").html(result.TotalStudents.Students);
                $("#lblComputers").html(result.TotalComputers.Computers);

                //Toilets
                $("#lblToiletStudentRatio").html(result.RatioStudentComfortRoom.StudentComfortRoomRatio);
                $("#lblComfortRoomRatio").html(result.TotalComfortRooms.ComfortRooms / result.TotalComfortRooms.ComfortRooms);
                $("#lblStudentToilet").html(result.TotalStudents.Students);
                $("#lblToilets").html(result.TotalComfortRooms.ComfortRooms);

                //Seats
                $("#lblSeatStudentRatio").html(result.RatioStudentSeat.StudentSeatRatio);
                $("#lblSeatRatio").html(result.TotalSeats.Seats / result.TotalSeats.Seats);
                $("#lblStudentSeat").html(result.TotalStudents.Students);
                $("#lblSeats").html(result.TotalSeats.Seats);

                //---------------------- PIE Chart by Ethnicity-------------------

                data = {
                    labels: ["Ilonggo", "Ilocano", "Blaan", "Cebuano", "Manobo", "Mangyan", "T'boli", "Others"],
                    datasets: [{
                        backgroundColor: [
                            "#3498db",
                            "#2ecc71",
                            "#95a5a6",
                            "#9b59b6",
                            "#f1c40f",
                            "#e74c3c",
                            "#74dcec",
                            "#e83e8c"
                        ],
                        data: [result.ByIlonggo.Ilonggo, result.ByIlocano.Ilocano, result.ByBLaan.BLaan, result.ByCebuano.Cebuano, result.ByManobo.Manobo, result.ByMangyan.Mangyan, result.ByTBoli.TBoli, result.ByOthers.Others]
                    }]
                };

                ctx = $("#EthnicityChart")[0].getContext("2d");
                Chart.pluginService.register({
                    beforeRender: function (chart) {
                        if (chart.config.options.showAllTooltips) {
                            // create an array of tooltips
                            // we can't use the chart tooltip because there is only one tooltip per chart
                            chart.pluginTooltips = [];
                            chart.config.data.datasets.forEach(function (dataset, i) {
                                chart.getDatasetMeta(i).data.forEach(function (sector, j) {
                                    chart.pluginTooltips.push(new Chart.Tooltip({
                                        _chart: chart.chart,
                                        _chartInstance: chart,
                                        _data: chart.data,
                                        _options: chart.options.tooltips,
                                        _active: [sector]
                                    }, chart));
                                });
                            });

                            // turn off normal tooltips
                            chart.options.tooltips.enabled = false;
                        }
                    },
                    afterDraw: function (chart, easing) {
                        if (chart.config.options.showAllTooltips) {
                            // we don't want the permanent tooltips to animate, so don't do anything till the animation runs atleast once
                            if (!chart.allTooltipsOnce) {
                                if (easing !== 1)
                                    return;
                                chart.allTooltipsOnce = true;
                            }

                            // turn on tooltips
                            chart.options.tooltips.enabled = true;
                            Chart.helpers.each(chart.pluginTooltips, function (tooltip) {
                                tooltip.initialize();
                                tooltip.update();
                                // we don't actually need this since we are not animating tooltips
                                tooltip.pivot();
                                tooltip.transition(easing).draw();
                            });
                            chart.options.tooltips.enabled = false;
                        }
                    }
                });
                var myPieChart = new Chart(ctx, {
                    type: 'pie',
                    data: data
                });


                //---------------------- Hor Bar by Religion-------------------
                var ctx = $("#ReligionChart")[0];

                //Chart.defaults.global.defaultFontFamily = "Lato";
                //Chart.defaults.global.defaultFontSize = 18;

                var ReligionData = {
                label: 'Religions',
                data: [result.ByIslam.Islam, result.ByCatholic.Catholic,result.ByEvangelical.Evangelical,result.ByINC.INC,result.BySDA.SDA,result.ByUCCP.UCCP,result.ByJehova.Jehova,result.ByMethodist.Methodist,result.ByBaptist.Baptist,result.ByTribal.Tribal, 0],
                backgroundColor: [
                "#3498db",
                "#2ecc71",
                "#95a5a6",
                "#9b59b6",
                "#f1c40f",
                "#e74c3c",
                "#74dcec",
                "#20c997",
                "#e83e8c",
                "#343a40"
                ],
                //borderColor: [
                //"#2ecc71",
                //"#2ecc71"
                //],
                //borderWidth: 2,
                //hoverBorderWidth: 0
                };
                
                var chartOptions = {
                scales: {
                yAxes: [{
                barPercentage: 0.5
                }]
                },
                elements: {
                //rectangle: {
                //borderSkipped: 'left',
                //}
                }
                };
 
                var barChart = new Chart(ctx, {
                type: 'horizontalBar',
                data: {
                labels: ["Islam", "Catholic", "Evangelical", "INC", "SDA", "UCCP", "Jehova", "Methodist", "Baptists", "Tribal"],
                datasets: [ReligionData],
                },
                options: chartOptions
                });
                

                //-------------By Track Bar Chart--------------//

                var ctx = $("#TrackChart")[0];

                //Chart.defaults.global.defaultFontFamily = "Lato";
                //Chart.defaults.global.defaultFontSize = 18;

                var MaleData = {
                    label: 'Boys',
                    data: [result.MaleTrack.MaleAcademic, result.MaleTrack.MaleTVL, result.MaleTrack.MaleSports, result.MaleTrack.MaleArtsDesign, 0],
                    backgroundColor: "#3498db",
                    borderWidth: 0,
                    yAxisID: "y-axis-Boys"
                };

                var FemaleData = {
                    label: 'Girls',
                    data: [result.FemaleTrack.FemaleAcademic, result.FemaleTrack.FemaleTVL, result.FemaleTrack.FemaleSports, result.FemaleTrack.FemaleArtsDesign, 0],
                    backgroundColor: "#2ecc71",
                    borderWidth: 0,
                    yAxisID: "y-axis-Girls"
                };

                var TrackData = {
                    labels: ["Academic", "TVL", "Sports", "Arts & Design"],
                    datasets: [MaleData, FemaleData]
                };

                var chartOptions = {
                    scales: {
                        xAxes: [{
                            barPercentage: 1,
                            categoryPercentage: 0.6
                        }],
                        yAxes: [{
                            id: "y-axis-Boys"
                        }, {
                            id: "y-axis-Girls"
                        }]
                    }
                };

                var barChart = new Chart(ctx, {
                    type: 'bar',
                    data: TrackData,
                    options: chartOptions
                });

                //-------------By Strand Bar Chart--------------//

                var ctx = $("#StrandChart")[0];

                //Chart.defaults.global.defaultFontFamily = "Lato";
                //Chart.defaults.global.defaultFontSize = 18;

                var MaleData = {
                    label: 'Boys',
                    data: [result.MaleStrand.MaleABM, result.MaleStrand.MaleHUMSS, result.MaleStrand.MaleSTEM, result.MaleStrand.MaleGAS, 0],
                    backgroundColor: "#3498db",
                    borderWidth: 0,
                    yAxisID: "y-axis-Boys"
                };

                var FemaleData = {
                    label: 'Girls',
                    data: [result.FemaleStrand.FemaleABM, result.FemaleStrand.FemaleHUMSS, result.FemaleStrand.FemaleSTEM, result.FemaleStrand.FemaleGAS, 0],
                    backgroundColor: "#2ecc71",
                    borderWidth: 0,
                    yAxisID: "y-axis-Girls"
                };

                var StrandData = {
                    labels: ["ABM", "HUMSS", "STEM", "GAS"],
                    datasets: [MaleData, FemaleData]
                };

                var chartOptions = {
                    scales: {
                        xAxes: [{
                            barPercentage: 1,
                            categoryPercentage: 0.6
                        }],
                        yAxes: [{
                            id: "y-axis-Boys"
                        }, {
                            id: "y-axis-Girls"
                        }]
                    }
                };

                var barChart = new Chart(ctx, {
                    type: 'bar',
                    data: StrandData,
                    options: chartOptions
                });

                //-------------By AcadStatus Bar Chart--------------//

                var ctx = $("#AcadStatusChart")[0];

                //Chart.defaults.global.defaultFontFamily = "Lato";
                //Chart.defaults.global.defaultFontSize = 18;

                var MaleData = {
                    label: 'Boys',
                    data: [result.MaleAcadStatus.MaleRegular, result.MaleAcadStatus.MaleIrregular, result.MaleAcadStatus.MaleTransferee, result.MaleAcadStatus.MaleBalikAral, 0],
                    backgroundColor: "#3498db",
                    borderWidth: 0,
                    yAxisID: "y-axis-Boys"
                };

                var FemaleData = {
                    label: 'Girls',
                    data: [result.FemaleAcadStatus.FemaleRegular, result.FemaleAcadStatus.FemaleIrregular, result.FemaleAcadStatus.FemaleTransferee, result.FemaleAcadStatus.FemaleBalikAral, 0],
                    backgroundColor: "#2ecc71",
                    borderWidth: 0,
                    yAxisID: "y-axis-Girls"
                };

                var AcadStatusData = {
                    labels: ["Regular", "Irregular", "Transferee", "Balik Aral"],
                    datasets: [MaleData, FemaleData]
                };

                var chartOptions = {
                    scales: {
                        xAxes: [{
                            barPercentage: 1,
                            categoryPercentage: 0.6
                        }],
                        yAxes: [{
                            id: "y-axis-Boys"
                        }, {
                            id: "y-axis-Girls"
                        }]
                    }
                };

                var barChart = new Chart(ctx, {
                    type: 'bar',
                    data: AcadStatusData,
                    options: chartOptions
                });

                //-------------By GradeLevel Bar Chart--------------//

                var ctx = $("#GradeLevelChart")[0];

                //Chart.defaults.global.defaultFontFamily = "Lato";
                //Chart.defaults.global.defaultFontSize = 18;

                var MaleData = {
                    label: 'Boys',
                    data: [result.MaleGradeLevel.MaleGradeLevel11, result.MaleGradeLevel.MaleGradeLevel12, 0],
                    backgroundColor: "#3498db",
                    borderWidth: 0,
                    yAxisID: "y-axis-Boys"
                };

                var FemaleData = {
                    label: 'Girls',
                    data: [result.FemaleGradeLevel.FemaleGradeLevel11,result.FemaleGradeLevel.FemaleGradeLevel12, 0],
                    backgroundColor: "#2ecc71",
                    borderWidth: 0,
                    yAxisID: "y-axis-Girls"
                };

                var GradeLevelData = {
                    labels: ["Grade 11", "Grade 12"],
                    datasets: [MaleData, FemaleData]
                };

                var chartOptions = {
                    scales: {
                        xAxes: [{
                            barPercentage: 1,
                            categoryPercentage: 0.6
                        }],
                        yAxes: [{
                            id: "y-axis-Boys"
                        }, {
                            id: "y-axis-Girls"
                        }]
                    }
                };

                var barChart = new Chart(ctx, {
                    type: 'bar',
                    data: GradeLevelData,
                    options: chartOptions
                });


                //-------------By Is Bar Chart--------------//

                var ctx = $("#ByIsChart")[0];

                //Chart.defaults.global.defaultFontFamily = "Lato";
                //Chart.defaults.global.defaultFontSize = 18;

                var MaleData = {
                    label: 'Boys',
                    data: [result.IsAssistanceMale.IsAssistance, result.Is4PsMale.Is4Ps, result.IsProvisinaryMale.IsProvisinary, 0],
                    backgroundColor: "#3498db",
                    borderWidth: 0,
                    yAxisID: "y-axis-Boys"
                };

                var FemaleData = {
                    label: 'Girls',
                    data: [result.IsAssistanceFemale.IsAssistance, result.Is4PsFemale.Is4Ps, result.IsProvisinaryFemale.IsProvisinary, 0],
                    backgroundColor: "#2ecc71",
                    borderWidth: 0,
                    yAxisID: "y-axis-Girls"
                };

                var ByIsData = {
                    labels: ["City Assistance", "4Ps/CCT", "Provisionary"],
                    datasets: [MaleData, FemaleData]
                };

                var chartOptions = {
                    scales: {
                        xAxes: [{
                            barPercentage: 1,
                            categoryPercentage: 0.6
                        }],
                        yAxes: [{
                            id: "y-axis-Boys"
                        }, {
                            id: "y-axis-Girls"
                        }]
                    }
                };

                var barChart = new Chart(ctx, {
                    type: 'bar',
                    data: ByIsData,
                    options: chartOptions
                });


            } //end Charts

        });
    } //end of function
});  //end of document

