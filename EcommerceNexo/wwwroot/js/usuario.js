$(document).ready(function () {

    $(".btn-delete").click(function () {
        let id = $(this).data("id");

        Swal.fire({
            title: "Tem certeza?",
            text: "Esta ação não poderá ser desfeita!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Sim, excluir",
            cancelButtonText: "Cancelar"
        }).then((result) => {
            if (result.isConfirmed) {
                deleteUsuario(id);
            }
        });
    });

    $("#frmEdit").on("submit", function (e) {
        editUsuario(e);
    });

    $("#frmFilter input, #frmFilter select").on('keyup change', function (e) {
        listUsuarios(e);
    });
});

function deleteUsuario(id) {
    $.ajax({
        url: '/Usuario/Delete',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(id),
        success: function (response) {
            if (response.success) {
                $("#usuario-" + id).remove();

                Swal.fire({
                    title: "Excluído!",
                    text: response.message,
                    icon: "success",
                    confirmButtonText: "OK",
                    customClass: { confirmButton: "btn btn-primary" }
                }).then(function () {
                    window.location.href = "/Usuario/Index";
                });
            } else {
                Swal.fire({
                    title: "Erro",
                    text: response.message,
                    icon: "error",
                    confirmButtonText: "OK",
                    customClass: { confirmButton: "btn btn-danger" }
                });
            }
        },
        error: function () {
            Swal.fire({
                title: "Erro inesperado",
                text: "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                icon: "error",
                confirmButtonText: "OK",
                customClass: { confirmButton: "btn btn-danger" }
            });
        }
    });
}

function editUsuario(e) {
    e.preventDefault();

    $(".is-invalid").removeClass("is-invalid");
    $(".invalid-feedback").text("");

    let usuario = {
        Id: $("#id").val(),
        Nome: $("#nome").val(),
        Email: $("#email").val()
    };

    //let senha = $("#senha").val();
    //if (senha && senha.trim() !== "") {
    //    usuario.Senha = senha;
    //}

    let senha = $("#senha").val();
    let confirmacaoSenha = $("#confirmacaoSenha").val();
    if ((senha && senha.trim() !== "") || (confirmacaoSenha && confirmacaoSenha.trim() !== "")) {
        usuario.Senha = senha;
        usuario.ConfirmacaoSenha = confirmacaoSenha;
    }

    let perfil = $("#perfil").val();
    if (perfil) {
        usuario.Perfil = perfil;
    }

    $.ajax({
        url: '/Usuario/Update',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(usuario),
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    title: "Atualizado!",
                    text: response.message,
                    icon: "success",
                    confirmButtonText: "OK",
                    customClass: { confirmButton: "btn btn-primary" }
                }).then(function () {
                    if (response.role === "admin") {
                        window.location.href = "/Usuario/Index";
                    } else {
                        window.location.href = "/Usuario/Edit/" + response.id;
                    }
                });
            } else if (response.erros) {
                $.each(response.erros, function (campo, mensagem) {
                    let input = $("#" + campo);
                    input.addClass("is-invalid");
                    $("#txtFeedback" + campo.charAt(0).toUpperCase() + campo.slice(1)).text(mensagem);
                });              

            } else if (response.message) {
                Swal.fire({
                    title: "Erro",
                    text: response.message,
                    icon: "error",
                    confirmButtonText: "OK",
                    customClass: { confirmButton: "btn btn-danger" }
                });
            }
        },
        error: function () {
            Swal.fire({
                title: "Erro inesperado",
                text: "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                icon: "error",
                confirmButtonText: "OK",
                customClass: { confirmButton: "btn btn-danger" }
            })
        }
    });
}

function listUsuarios(e) {
    e.preventDefault();

    let nome = $("#nome").val();
    let email = $("#email").val();
    let perfil = $("#perfil").val();

    $.ajax({
        url: "/Usuario/FilterUsers",
        type: 'GET',
        data: {
            nome: nome,
            email: email,
            perfil: perfil
        },
        success: function (response) {
            $("#usuariosTable").html(response);
        },
        error: function () {
            Swal.fire({
                title: "Erro inesperado",
                text: "Ocorreu um erro ao listar os usuários. Tente novamente mais tarde.",
                icon: "error",
                confirmButtonText: "OK",
                customClass: { confirmButton: "btn btn-danger" }
            })
        }
    });
}