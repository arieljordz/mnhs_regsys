$(document).ready(function () {
    var date = new Date();
    $('.mask-money').mask('000,000,000,000,000.00', { reverse: true, placeholder: "0.00" });
    $('.mask-phone').mask('(000) 000-0000', { placeholder: "(___) ___-____" });
    $('.mask-mobile').mask('+63 999-999-9999', { placeholder: "+63 ___-___-____" });
    $('.mask-tin').mask('000-000-000-000', { placeholder: "___-___-___-___" });
    $('.mask-LandPIN').mask('000-00-0000-00-000-0000', { placeholder: "___-__-___-__-___-____" });
    //$('.mask-psn').mask('PSN-' + date.getFullYear() + '-000000', { placeholder: "___-____-______" });
    $('.mask-ControlNo').mask('BLG-0000-19-0000019', { placeholder: "BLG-____-__-_______" });
    $('.mask-PSNo').mask('PSN-0000-0000019', { placeholder: "xxx-____-_______" });
    //$('.mask-ctc').mask('CCI0000 00000000', { placeholder: "CCI____ ________" });
    $('.mask-tdn').mask('00-000-00000', { placeholder: "__-___-_____" });
    $('.mask-tct').mask('T-0000', { placeholder: "T-____" });
    $('.mask-lrn').mask('000000000000', { placeholder: "____________" });
    //$('.mask-lpin').mask('000-00-000-00-000-0000', { placeholder: "___-__-___-__-___-____" });
    //$('.mask-lpin').removeAttr("maxlength");
    //$('.mask-bpin').mask('0000', { placeholder: "____" });
    //$('.mask-cardNo').mask('0000-0000-0000-0000', { placeholder: "____-____-____-____" });
    //$('.mask-cvv').mask('000', { placeholder: "___" });
});