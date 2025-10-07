$(document).ready(function () {
    $('#frmRegister').on('submit', function (e) {
        Cadastrar(e);
    });

    $("#frmLogin").on("submit", function (e) {
        Login(e);
    });
});


function Cadastrar(e) {
    e.preventDefault();

    $(".is-invalid").removeClass("is-invalid");
    $(".invalid-feedback").text("");

    let usuario = {
        Nome: $("#nome").val(),
        Email: $("#email").val(),
        Senha: $("#senha").val(),
        ConfirmacaoSenha: $("#confirmacaoSenha").val()
    }

    $.ajax({
        url: '/Usuario/Create',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(usuario),
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    title: "Sucesso!",
                    text: response.message,
                    icon: "success",
                    confirmButtonText: "OK",
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                }).then(function () {
                    window.open('/Home/Index', '_self');
                });
            }
            else if (response.erros) {
                $.each(response.erros, function (campo, mensagem) {
                    let input = $("#" + campo);
                    input.addClass("is-invalid");
                    $("#txtFeedback" + campo.charAt(0).toUpperCase() + campo.slice(1)).text(mensagem);
                });
            }
            else if (response.message) {
                Swal.fire({
                    title: "Erro",
                    text: response.message,
                    icon: "error",
                    confirmButtonText: "OK",
                    customClass: {
                        confirmButton: "btn btn-danger"
                    }
                });
            }
        },
        error: function () {
            Swal.fire({
                title: "Erro inesperado",
                text: "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                icon: "error",
                confirmButtonText: "OK",
                customClass: {
                    confirmButton: "btn btn-danger"
                }
            });
        }
    });
}


function Login(e) {
    e.preventDefault();

    $("#loginError").addClass("d-none").text("");

    let usuario = {
        Email: $("#email").val(),
        Password: $("#senha").val()
    };

    $.ajax({
        url: '/Usuario/Login',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(usuario),
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    title: "Bem vindo!",
                    text: response.message,
                    icon: "success",
                    confirmButtonText: "OK",
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                }).then(function () {
                    window.open('/Home/Index', '_self');
                });
            }
            else {
                $("#loginError").removeClass("d-none").text(response.message);
            }
        },
        error: function () {
            Swal.fire({
                title: "Erro inesperado",
                text: "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                icon: "error",
                confirmButtonText: "OK",
                customClass: {
                    confirmButton: "btn btn-danger"
                }
            });
        }
    });
}