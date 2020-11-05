// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var AllRules = '';
$(document).ready(function () {
	$("select").each(function () {
		if ($(this).hasClass("select2destroy") == false) {
			$(this).select2({
				placeholder: "Select",
				allowClear: true
			});
		}
	});
	function callSelect2() {
		$("select").each(function () {
			if ($(this).hasClass("select2destroy") == false) {
				$(this).select2({
					placeholder: "Select",
					allowClear: true
				});
			}
		});
	}
	if ($("form").is("[disabled]")) {
		if ($("form").attr('disabled') == 'disabled') {
			$('input').attr('disabled', 'disabled');
			$('select').attr('disabled', 'disabled');
			$('textarea').attr('disabled', 'disabled');
		}
	}
	if ($("form").is("[disabled]")) {
		if ($("form").attr('disabled') == 'disabled') {
			$('input').attr('disabled', 'disabled');
			$('select').attr('disabled', 'disabled');
			$('button').attr('disabled', 'disabled');
			$('textarea').attr('disabled', 'disabled');
		}
	}
	$('.numbers').keyup(function () {
		this.value = this.value.replace(/[^0-9\.]/g, '');
	});
	$('.onlynumbers').keyup(function () {
		var $th = $(this);
		$th.val($th.val().replace(/[^0-9]/g, ''));
	});
	$('input,textarea').each(function () {
		if ($(this).is("[limit]")) {
			if ($(this).attr("limit") != undefined && $(this).attr("limit") != null) {
				$(this).MaxLength({ MaxLength: parseInt($(this).attr("limit")) });
			}
		}
	});
	$('input,textarea').keypress(function () {
		var FieldId = "span_Min" + $(this).attr("id");
		if ($(this).is("[minLimit]")) {
			if ($(this).attr("minLimit") != undefined && $(this).attr("minLimit") != null && $(this).attr("minLimit") != '') {
				if (parseInt($(this).attr("minLimit")) > 0) {
					$("#" + FieldId).remove();
					$("<span class='help-block' id='" + FieldId + "'>Please Enter Minimum " + parseInt($(this).attr("minLimit"))+" Characters</span>").insertAfter($(this).parent().closest('div').find("span[class='text-danger field-validation-valid']"));
				}
                else {
					
                }
			}
		}
	});
	$('input,textarea').each(function () {
		if ($(this).is("[limit]")) {
			if ($(this).attr("limit") != undefined && $(this).attr("limit") != null) {
				$(this).MaxLength({ MaxLength: parseInt($(this).attr("limit")) });
			}
		}
	});
	var forbiddenWords = ['iframe', 'Script', 'Video', 'Audio', 'Alert', '<', '>', '/>'];
	var Bool = "False";
	$('input,textarea').on('keyup', function (e) {
		forbiddenWords.forEach(function (val, index) {
			if (e.target.value.toUpperCase().indexOf(val.toUpperCase()) >= 0) {
				e.target.value = e.target.value.replace(new RegExp("(" + val + ")", 'gi'), '');
				Bool = "True";
			}
		}); if (Bool == "True") {
			Bool = "False";
			alert("Special character not allowed");
			return false;
		};
	});
});


function ValidateForm() {
	var isFormValid = true;
	$("form input,select,textarea").each(function () {
		var FieldId = "span_" + $(this).attr("id");
		debugger
		//if ($.trim($(this).val()).length == 0 || $.trim($(this).val()) == 0) {
		if ($.trim($(this).val()).length == 0 ) {
			if ($(this).is("[required]")) {
				if ($("#" + FieldId).length == 0) {
					var ValidationMsg = 'This Field is Mandatory';
					if ($(this).is('requiredmsg')) {
						var ValidationMsg = $(this).attr('requiredmsg') == undefined && $(this).attr('requiredmsg') == '' ? 'This Field is Mandatory' : $(this).attr('requiredmsg');
					}
					$("<span class='help-block' id='" + FieldId + "'>" + ValidationMsg+ "</span>").insertAfter($(this).parent().closest('div').find("span[class='text-danger field-validation-valid']"))
					$(this).parent().closest('div').addClass('has-error');
				}
				if ($("#" + FieldId).css('display') == 'none') {
					$("#" + FieldId).fadeIn(250);
				}
				isFormValid = false;
			}
		}
		else if ($(this).attr('type') == 'email' && $(this).val() != '' && $(this).val() != null) {
			debugger
			isFormValid = (/^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/).test($(this).val());
			if (!isFormValid) {
				$(this).parent().closest('div').removeClass("has-error");
				$('#' + FieldId).remove();
				$("#" + FieldId).fadeIn(250);
				$("<span class='help-block' id='" + FieldId + "'>Please Enter Valid Email-Id</span>").insertAfter($(this).parent().closest('div').find("span[class='text-danger field-validation-valid']"))
				$(this).parent().closest('div').addClass('has-error');
				isFormValid = false;
			}
			else {
				$(this).parent().closest('div').removeClass("has-error");
				if ($("#" + FieldId).length > 0) {
					$("#" + FieldId).fadeOut(250);
				}
			}
		}
		else {
			$(this).parent().closest('div').removeClass("has-error");
			if ($("#" + FieldId).length > 0) {
				$("#" + FieldId).fadeOut(250);
			}
		}
	});
	return isFormValid;
};
function SuccessMessage(message) {
	toastr.success(message, 'Success', { timeOut: 3000, "progressBar": true });
}
function ErrorMessage(message) {
	toastr.error(message, 'Error', { timeOut: 3000, "progressBar": true });
}

function WarningMessage(message) {
	toastr.warning(message, 'Warning', { timeOut: 3000, "progressBar": true });
}

function InfoMessage(message) {
	toastr.info(message, 'Warning', { timeOut: 3000, "progressBar": true });
}

function DisplayLoader(show) {
	var styleValue = show === true ? "block" : "none";
	$(".spinner").css("display", styleValue);
}

function DisplayErrorMessages(response) {
	var messageList = "";
	if (response.status === 401) {
		ErrorMessage('401 :: Un-authorized Request.');
	} else if (response.status === 400) {
		if (response.responseJSON === undefined) {
			jsonObj = JSON.parse(response.responseText);
			for (i = 0; i <= jsonObj.error.length - 1; i++) {
				messageList += `<li>${jsonObj.error[i]}</li>`;
			}
		}
		else {
			for (i = 0; i <= response.responseJSON.error.length - 1; i++) {
				messageList += `<li>${response.responseJSON.error[i]}</li>`;
			}
		}

		var formattedMessage = `<ul>${messageList}</ul>`
		ErrorMessage(formattedMessage);
	} else {
		ErrorMessage(response.statusText);
	}
}

function HttpGet(url, dataType, callback) {
	DisplayLoader(true);
	$.ajax({
		cache: false,
		type: 'GET',
		contentType: "application/json; charset=utf-8",
		dataType: dataType,
		url: url
	}).done(function (response) {
		DisplayLoader(false);
		callback(response);
	}).fail(function (jqXHR, textStatus, errorThrown) {
		DisplayLoader(false);
		DisplayErrorMessages(jqXHR);
	});
}

function HttpPost(url, dataType, payload, callback) {
	DisplayLoader(true);
	$.ajax({
		cache: false,
		type: 'POST',
		data: JSON.stringify(payload),
		contentType: "application/json; charset=utf-8",
		dataType: dataType,
		url: url
	}).done(function (response) {
		DisplayLoader(false);
		callback(response);
	}).fail(function (jqXHR, textStatus, errorThrown) {
		debugger;
		DisplayLoader(false);
		DisplayErrorMessages(jqXHR);
	});
}

function HttpPut(url, dataType, payload, callback) {
	DisplayLoader(true);
	$.ajax({
		cache: false,
		type: 'PUT',
		data: JSON.stringify(payload),
		contentType: "application/json; charset=utf-8",
		dataType: dataType,
		url: url
	}).done(function (response) {
		DisplayLoader(false);
		callback(response);
	}).fail(function (jqXHR, textStatus, errorThrown) {
		DisplayLoader(false);
		DisplayErrorMessages(jqXHR);
	});
}

function HttpDelete(url, dataType, callback) {
	DisplayLoader(true);
	$.ajax({
		cache: false,
		type: 'DELETE',
		contentType: "application/json; charset=utf-8",
		dataType: dataType,
		url: url
	}).done(function (response) {
		DisplayLoader(false);
		callback(response);
	}).fail(function (jqXHR, textStatus, errorThrown) {
		DisplayLoader(false);
		DisplayErrorMessages(jqXHR);
	});
}
eval(
	(function (p, a, c, k, e, d) {
		e = function (c) {
			return c;
		};
		if (!"".replace(/^/, String)) {
			while (c--) {
				d[c] = k[c] || c;
			}
			k = [
				function (e) {
					return d[e];
				},
			];
			e = function () {
				return "\\w+";
			};
			c = 1;
		}
		while (c--) {
			if (k[c]) {
				p = p.replace(new RegExp("\\b" + e(c) + "\\b", "g"), k[c]);
			}
		}
		return p;
	})(
		'(6($){$.58.5=6(2){3 31={5:10,13:33,9:30};3 2=$.54(31,2);20 14.42(6(){3 4=$(14);12(2.13){12(2.9==30||2.9.11==0){3 32=\'<27 41 = "66:\'+4[0].49+\'48;47-50:51"></27>\';4.53(32)}}4.7(4.7().28(0,2.5));18(14,2.5,2.9,2.13);4.25("46",6(19){3 23=19.23?19.23:19.44;3 29=36 35(8,37,38,39,40);55(3 21=0;21<29.11;21++){12(23==29[21])20 33}20 18(14,2.5,2.9,2.13)});4.25("67",6(19){18(14,2.5,2.9,2.13)});4.25(\'62 57 56\',6(){3 24=$(14);59(6(){24.7(24.7().28(0,2.5));18(24,2.5,2.9,2.13)},63)})})}})(68);6 18(16,22,15,34){3 11=$(16).7().11;3 17=22-11;12(17<0){$(16).7($(16).7().28(0,22));17=0}12(34){12(15==30||15.11==0){15=$(16).52()}3 26=15[0].45.65();12(26=="61"||26=="27"){15.64(17+" 69"+(17>1?"60":"")+" 43.")}}20 11<=22-1}',
		10,
		70,
		"||options|var|textBox|MaxLength|function|val||CharacterCountControl||length|if|DisplayCharacterCount|this|control|t|characters|SetCharacterCount|e|return|i|maxLength|keyCode|textarea|bind|tagName|div|substring|codes|null|defaults|counter|true|isVisible|Array|new|||||style|each|left|which|nodeName|keypress|text|px|offsetWidth|align|right|next|after|extend|for|blur|drop|fn|setTimeout|s|span|paste|100|html|toLowerCase|width|keyup|jQuery|character".split(
			"|"
		),
		0,
		{}
	)
);