//Get Notificações
function getNotificacoes() {
	setInterval(function () {

		$.ajax({
			type: "GET",
			url: "/Notificacoes/getNotificacoes/",
			data: null,
			success: function (result) {
				if (result.sucesso) {
					
					for (var i = 0; i < result.lista.length; i++) {
						notify("bottom", "right", "", "info", "animated bounceIn", "animated bounceOut", result.lista[i].funcionarioCriador.nome + "!", result.lista[i].mensagem, i * 3000, result.lista[i].urlRedirect);
					}

				} else {
					jsAbrePartial("Atenção!", result.mensagem, "warning");
				}
			}
		});
	}, 60000);
}


/*--------------------------------------
    Bootstrap Notify Notifications
---------------------------------------*/
function notify(from, align, icon, type, animIn, animOut, title, message, time, url) {
	$.notify({
		icon: icon,
		title: title,
		message: message,
		url: url
	}, {
		element: 'body',
		type: type,
		allow_dismiss: true,
		placement: {
			from: from,
			align: align
		},
		offset: {
			x: 20,
			y: 20
		},
		spacing: 10,
		z_index: 1031,
		delay: 25000 + time,
		timer: 1000,
		url_target: '_blank',
		mouse_over: false,
		animate: {
			enter: animIn,
			exit: animOut
		},
		template: '<div data-notify="container" class="alert alert-dismissible alert-{0} alert--notify" role="alert">' +
		'<span data-notify="icon"></span> ' +
		'<span data-notify="title">{1}</span> ' +
		'<span data-notify="message">{2}</span>' +
		'<div class="progress" data-notify="progressbar">' +
		'<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
		'</div>' +
		'<a href="{3}" target="{4}" data-notify="url"></a>' +
		'<button type="button" aria-hidden="true" data-notify="dismiss" class="alert--notify__close">CLOSE</button>' +
		'</div>'
	});
}

var focusedBeforeAlert;

function CloseAlert() {

	if (focusedBeforeAlert)
		focusedBeforeAlert.focus();

	$('#divModalGeral').modal('hide');
}

function closeModal(event) {
	$('.modalAlerta').addClass("animate-zoom-close")
	setTimeout(function () {
		$('.modalAlerta').removeClass("animate-zoom-close")
		$('.modalAlerta').hide();
	}, 500);
}

document.onkeydown = function (e) {
	if (e == null) {
		keycode = event.keyCode;
	}
	else {
		keycode = e.which;
	}

	if (keycode == 27) {

		CloseAlert();

	}

}

$('.noBlank').on('blur', function () {

	if ($(this).val() == '') {
		$(this).removeClass('form-control-success');
		$(this).parent().removeClass('has-success');
		$(this).addClass('form-control-danger');
		$(this).parent().addClass('has-danger');
	} else {
		$(this).removeClass('form-control-danger');
		$(this).parent().removeClass('has-danger');
		$(this).addClass('form-control-success');
		$(this).parent().addClass('has-success');
	}
});

function jsRemoverFocus(element) {
	$(element).removeClass('form-control-success');
	$(element).parent().removeClass('has-success');
	$(element).removeClass('form-control-danger');
	$(element).parent().removeClass('has-danger');
}

function jsLoading(show) {

	var objDivLoadBack = document.getElementById('divLoadBack');
	var objDivLoad = document.getElementById('divLoad');
	var objDivLoad2 = document.getElementById('divLoad2');

	if (objDivLoad != null) {
		if (show) {
			objDivLoadBack.style.display = '';
			objDivLoad.style.display = '';
			objDivLoad2.style.display = '';
		}
		else {
			objDivLoadBack.style.display = 'none';
			objDivLoad.style.display = 'none';
			objDivLoad2.style.display = 'none';
		}
	}
}

function removeAllChilds(obj) {

	if (obj.hasChildNodes()) {
		while (obj.childNodes.length >= 1) {
			obj.removeChild(obj.firstChild);
		}
	}

}

//ADICIONA CABEÇALHO EM UMA TABELA DINÂMICA
function jsAdicionarCabecalhoTabela(tabelaInserida, listaColunas, classAtt, styleAtt) {

	removeAllChilds(tabelaInserida);

	var thead = document.createElement('thead');
	thead.className = "thead-default";

	if (styleAtt != undefined) {
		thead.setAttribute("style", styleAtt);
	}

	tabelaInserida.appendChild(thead);

	var cabecalho = tabelaInserida.tHead;

	var trCabecalho = document.createElement('tr');

	for (var i = 0; i < listaColunas.length; i++) {
		var th = document.createElement('th');

		if (classAtt) {
			th.setAttribute('class', classAtt);
		}
		th.innerHTML = listaColunas[i];
		trCabecalho.appendChild(th);
	}

	cabecalho.appendChild(trCabecalho);

}

//ADICIONA COLUNAS NA LINHA DA TABELA DINÂMICA
function jsAdicionarLinhaTabela(listaColunas, linha, idObjeto, dsIdentificador) {

    var linhaCorrente = document.getElementById(idObjeto + dsIdentificador);

    if (linhaCorrente != null) {

        OpenAlert('Atenção', 'Não é possível inserir este registro, pois já está adicionado.');
        return false;

    } else {

        for (var i = 0; i < listaColunas.length; i++) {
            linha.appendChild(listaColunas[i]);
        }
    }
}

//ADICIONA CORPO NA TABELA
function jsAdicionarCorpoTabela(tabelaInserida) {

    var tbody = tabelaInserida.tBodies;

    if (tbody[0] == null) {
        tbody = document.createElement('tbody');

        tabelaInserida.appendChild(tbody);
    } else {
        tbody = tbody[0];
    }

    return tbody;

}

//Type - danger, info, warning, success
function OpenAlert(title, content, type) {

	focusedBeforeAlert = document.activeElement;

	if (navigator.appName == 'Microsoft Internet Explorer') {

		var ua = navigator.userAgent;
		var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
		if (re.exec(ua) != null)
			rv = parseFloat(RegExp.$1);

		if (rv == '10') {
			alert(content);
		} else {
			$('#spanModalTitle').html(title);
			$('#spanModalContent').html(content);
			$('#divModalGeral').attr('class', '');
			$('#divModalGeral').addClass('alert alert-' + type + ' alert-dismissable');
			$('#divModalGeral').modal('show');
			$('#btnModalCancel').css('display', 'none');
		}

	} else {
		$('#spanModalTitle').html(title);
		$('#spanModalContent').html(content);
		$('#divModalGeral').attr('class', '');
		$('#divModalGeral').addClass('alert alert-' + type + ' alert-dismissable');
		$('#divModalGeral').modal('show');
		$('#btnModalCancel').css('display', 'none');
	}

}

function CloseAlert() {

	if (focusedBeforeAlert)
		focusedBeforeAlert.focus();

	$('#divModalGeral').modal('hide');
}

//MASCÁRAS
//add maskCNPJ
$(".maskCNPJ").each(function () {
	$(this).mask("99.999.999/9999-99");
	$(this).on('blur', function () {
		if ($(this).val() != "")
		{
			if (jsValidaCNPJ($(this).val()) == false) {
				$(this).removeClass('form-control-success');
				$(this).parent().removeClass('has-success');
				$(this).addClass('form-control-danger');
				$(this).parent().addClass('has-danger');
				OpenAlert('Erro!', 'CNPJ inválido', 'danger');
				$(this).val('');
			} else {
				$(this).removeClass('form-control-danger');
				$(this).parent().removeClass('has-danger');
				$(this).addClass('form-control-success');
				$(this).parent().addClass('has-success');
			}
		}
	});
});

//MASK CPF
$(".maskCPF").each(function () {
	$(this).mask("999.999.999-99");
	$(this).on('blur', function () {
		if ($(this).val() != "") {
			if (jsValidaCPF($(this).val()) == false) {
				$(this).removeClass('form-control-success');
				$(this).parent().removeClass('has-success');
				$(this).addClass('form-control-danger');
				$(this).parent().addClass('has-danger');
				OpenAlert('Erro!', 'CPF inválido', 'danger');
				$(this).val('');
			} else {
				$(this).removeClass('form-control-danger');
				$(this).parent().removeClass('has-danger');
				$(this).addClass('form-control-success');
				$(this).parent().addClass('has-success');
			}
		}
	});
});

//add maskCEP
$(".maskCEP").each(function () {
	$(this).mask("99999-999");
});

//add maskTel
$(".maskTel").each(function () {
	$(this).mask("(99)99999-9999");
});

//add maskTelFixo
$(".maskTelFixo").each(function () {
	$(this).mask("(99)9999-9999");
});

//add maskTime
$(".maskTime").each(function () {
	$(this).mask("99:99");
});

$(".maskPhone8").each(function () {
	mascara(this, mtel);
});

$(".maskPhone").keyup(function () {
	mascara(this, mtel);
});

//add maskPlaca
$(".maskPlaca").each(function () {
	$(this).mask("aaa-9999");
});

//add maskMoney
$(".maskMoney").each(function () {
	$(this).maskMoney({ decimal: ",", thousands: "." });
});

//VALIDAÇÕES
function jsValidaCPF(cpf) {
	cpf = cpf.replace(/[^\d]+/g, '');
	if (cpf == '') return false;
	// Elimina CPFs invalidos conhecidos    
	if (cpf.length != 11 ||
		cpf == "00000000000" ||
		cpf == "11111111111" ||
		cpf == "22222222222" ||
		cpf == "33333333333" ||
		cpf == "44444444444" ||
		cpf == "55555555555" ||
		cpf == "66666666666" ||
		cpf == "77777777777" ||
		cpf == "88888888888" ||
		cpf == "99999999999")
		return false;
	// Valida 1o digito 
	add = 0;
	for (i = 0; i < 9; i++)
		add += parseInt(cpf.charAt(i)) * (10 - i);
	rev = 11 - (add % 11);
	if (rev == 10 || rev == 11)
		rev = 0;
	if (rev != parseInt(cpf.charAt(9)))
		return false;
	// Valida 2o digito 
	add = 0;
	for (i = 0; i < 10; i++)
		add += parseInt(cpf.charAt(i)) * (11 - i);
	rev = 11 - (add % 11);
	if (rev == 10 || rev == 11)
		rev = 0;
	if (rev != parseInt(cpf.charAt(10)))
		return false;
	return true;
}

function jsValidaCNPJ(cnpj) {

	cnpj = cnpj.replace(/[^\d]+/g, '');

	if (cnpj == '') return false;

	if (cnpj.length != 14)
		return false;

	// Elimina CNPJs invalidos conhecidos
	if (cnpj == "00000000000000" ||
		cnpj == "11111111111111" ||
		cnpj == "22222222222222" ||
		cnpj == "33333333333333" ||
		cnpj == "44444444444444" ||
		cnpj == "55555555555555" ||
		cnpj == "66666666666666" ||
		cnpj == "77777777777777" ||
		cnpj == "88888888888888" ||
		cnpj == "99999999999999")
		return false;

	// Valida DVs
	tamanho = cnpj.length - 2
	numeros = cnpj.substring(0, tamanho);
	digitos = cnpj.substring(tamanho);
	soma = 0;
	pos = tamanho - 7;
	for (i = tamanho; i >= 1; i--) {
		soma += numeros.charAt(tamanho - i) * pos--;
		if (pos < 2)
			pos = 9;
	}
	resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
	if (resultado != digitos.charAt(0))
		return false;

	tamanho = tamanho + 1;
	numeros = cnpj.substring(0, tamanho);
	soma = 0;
	pos = tamanho - 7;
	for (i = tamanho; i >= 1; i--) {
		soma += numeros.charAt(tamanho - i) * pos--;
		if (pos < 2)
			pos = 9;
	}
	resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
	if (resultado != digitos.charAt(1))
		return false;

	return true;

}

function jsSomenteNumero(e) {
	var tecla = (window.event) ? event.keyCode : e.which;
	if ((tecla > 47 && tecla < 58)) return true;
	else {
		if (tecla == 8 || tecla == 0) return true;
		else return false;
	}
}

function jsSomenteNumeroVirgula(e) {
	var tecla = (window.event) ? event.keyCode : e.which;
	if ((tecla > 47 && tecla < 58)) return true;
	else {
		if (tecla == 8 || tecla == 0 || tecla == 44) return true;
		else return false;
	}
}

function jsSomenteDecimal(e) {
	var tecla = (window.event) ? event.keyCode : e.which;
	if ((tecla > 47 && tecla < 58)) return true;
	else {
		if (tecla == 8 || tecla == 0 || tecla == 46 || tecla == 44) return true;
		else return false;
	}
}

//BUSCAR ENDEREÇOS NA BASE DE DADOS DOS CORREIOS
function GetEnderecoCorreio(cep, callBackFunc) {

	var cepSemMascara = cep.replace(/[^\d]+/g, '');

	if (cepSemMascara == "") {
		return false;
	}

	jsLoading(true);

	//CONSULTA VIACEP
	$.ajax({
		url: "//viacep.com.br/ws/" + cepSemMascara + "/json/?callback=?",
		dataType: 'json',
		async: false,
		success: function (data) {
			jsLoading(false);
			callBackFunc(data);
		},
		error: function (msg) {
			jsLoading(false);
			OpenAlert("Atenção!", "Não foi possível encontrar os dados do CEP informado!", "danger");
		}
	});

	//CONSULTA API CORREIOS
	//$.ajax({
	//	url: url,
	//	data: null,
	//	type: 'GET',
	//	crossDomain: true,
	//	dataType: 'jsonp',
	//	contentType: "application/json",
	//	statusCode: {
	//		200: function (data) {
	//			return data;
	//			jsLoading(false);
	//		}
	//		, 400: function (msg) {
	//			jsLoading(false);
	//			OpenAlert("Atenção!", "Não foi possível realizar a consulta! Consulte o suporte.", "danger");
	//		}
	//		, 404: function (msg) {
	//			jsLoading(false);
	//			OpenAlert("Atenção!", "Não foi possível encontrar os dados do CEP informado!", "danger");
	//		}
	//	}
	//});
}

function GetDadosReceitaFederal(cnpj, callbackFunc) {

	var cnpjSemMascara = cnpj.replace(/[^\d]+/g, '');

	if (cnpjSemMascara == "") {
		return false;
	}

	var url = "https://www.receitaws.com.br/v1/cnpj/" + cnpjSemMascara;

	jsLoading(true);

	$.ajax({
		url: url,
		data: null,
		type: 'GET',
		crossDomain: true,
		dataType: 'jsonp',
		success: function (data) {

			if (data.status == "OK") {
				//Preencher CAMPOS com os dados retornados
				callbackFunc(data);

				jsLoading(false);
			} else {
				jsLoading(false);
				OpenAlert("Atenção!", data.message, "danger");
			}
		},
		error: function () {
			jsLoading(false);
			OpenAlert("Atenção!", "Não foi possível encontrar os dados do CNPJ informado!", "danger");
		}
	});

}

function preencheDDLCidades(codigoEstado, $el, codigoCidade) {

	//$el -> representa a ddlCidade

	if (codigoEstado != "") {
		jsLoading(true);

		var url = "/Cliente/GetCidade/";
		var data = {
			Estado: codigoEstado
		};
		$.ajax({
			type: "GET",
			url: "/Cidade/GetCidade/",
			data: data,
			async: false,
			success: function (data) {
				if (data) {
					$el.empty(); // remove old options
					$el.append($("<option></option>")
						.attr("value", '').text('Todas as Cidades'));
					$.each(data, function (i, obj) {
						$el.append($("<option></option>")
							.attr("value", obj.value).text(obj.text));
					});
					if (codigoCidade) {
						$el.val(codigoCidade);
					}
					$el.trigger("change");

					jsLoading(false);
				}
			}
		});
	} else {
		$el.empty();
		$el.append($("<option></option>")
			.attr("value", '').text('Todas as Cidades'));
		$el.trigger("change");
	}
}

//CONFIGURA CHECK
$('.check').each(function () {
	if ($($(this).children()[0]).is(':checked')) {
		$(this).addClass('active');
	}
});

$('input').change(function () {
	if ($(this).val().length > 0) {
		$(this).addClass('form-control--active');
		$(this).removeClass('form-control-danger');
		$(this).parent().removeClass('has-danger');
		$(this).addClass('form-control-success');
		$(this).parent().addClass('has-success');
	} else {
		$(this).removeClass('form-control--active');
		$(this).removeClass('form-control-success');
		$(this).parent().removeClass('has-success');
		$(this).addClass('form-control-danger');
		$(this).parent().addClass('has-danger');
	}
});

function getCookie(cname) {
	var name = cname + "=";
	var ca = document.cookie.split(';');
	for (var i = 0; i < ca.length; i++) {
		var c = ca[i];
		while (c.charAt(0) == ' ') {
			c = c.substring(1);
		}
		if (c.indexOf(name) == 0) {
			return c.substring(name.length, c.length);
		}
	}
	return "";
}

function mascaraValor(valor) {
	valor = valor.toString().replace(/\D/g, "");
	valor = valor.toString().replace(/(\d)(\d{8})$/, "$1.$2");
	valor = valor.toString().replace(/(\d)(\d{5})$/, "$1.$2");
	valor = valor.toString().replace(/(\d)(\d{2})$/, "$1,$2");
	return valor;
}

//Máscara Telefone
/* Máscaras ER */
function mascara(o, f) {
	v_obj = o
	v_fun = f
	setTimeout("execmascara()", 1)
}
function execmascara() {
	v_obj.value = v_fun(v_obj.value)
}
function mtel(v) {
	v = v.replace(/\D/g, "");             //Remove tudo o que não é dígito
	v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
	v = v.replace(/(\d)(\d{4})$/, "$1-$2");    //Coloca hífen entre o quarto e o quinto dígitos
	return v;
}
function id(el) {
	return document.getElementById(el);
}

function jsAbreMotivoRetorno(motivo) {
	swal("Motivo Retorno", motivo, "info");
}