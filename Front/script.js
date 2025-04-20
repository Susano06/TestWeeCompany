//Inicialización (Document Ready)
$(document).ready(function() {
    // Validación en tiempo real
    $('#registroForm input').on('input', function() {
        validateField($(this));
        checkFormCompletion();
    });
    
    //Valida el checkbox de términos cuando cambia su estado.
    $('#terminos').change(function() {
        checkFormCompletion();
    });
      
    // Envío del formulario
    $('#registroForm').submit(function(e) {
        e.preventDefault();
        if ($('#guardarBtn').prop('disabled')) return;
        
        const formData = {
            compania: $('#compania').val(),
            cedula: $('#cedula').val(),
            nombre: $('#nombre').val(),
            titulo: $('#titulo').val(),
            email: $('#email').val(),
            telefono: $('#telefono').val(),
            terminos: $('#terminos').is(':checked')
        };
        enviarDatos(formData);
    });
    
    // Eventos para modales
    $('.close').click(function() {
        $(this).closest('.modal').hide();
    });
    
    $('#aceptarBtn').click(function() {
        $('#confirmModal').hide();
        obtenerRegistros();
    });
    
    $('#errorAceptarBtn').click(function() {
        $('#errorModal').hide();
    });
    
    // Funciones de validación
    function validateField(field) {
        let isValid = true;
        const value = field.val().trim();
        const errorSpan = field.siblings('.error-message');
        
        errorSpan.hide();
        field.removeClass('invalid');
        
        if (field.attr('required') && value === '') {
            showError(field, errorSpan, 'Este campo es requerido');
            return false;
        }
        
        switch(field.attr('id')) {
            case 'compania':
                if (!/^[a-zA-Z0-9\s]+$/.test(value)) {
                    showError(field, errorSpan, 'Solo letras y números permitidos');
                    isValid = false;
                }
                break;
            case 'nombre':
                if (!/^[a-zA-Z\sáéíóúÁÉÍÓÚñÑ]+$/.test(value)) {
                    showError(field, errorSpan, 'Solo letras permitidas');
                    isValid = false;
                }
                break;
            case 'cedula':
                if (!/^\d+$/.test(value)) {
                    showError(field, errorSpan, 'Solo números permitidos');
                    isValid = false;
                }
                break;
            case 'email':
                if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value)) {
                    showError(field, errorSpan, 'Ingrese un correo válido');
                    isValid = false;
                }
                break;
            case 'telefono':
                if (!/^\d+$/.test(value)) {
                    showError(field, errorSpan, 'Solo números permitidos');
                    isValid = false;
                }
                break;
        }
        
        return isValid;
    }
    
    //Muestra mensajes de error y resalta campos inválidos.
    function showError(field, errorSpan, message) {
        errorSpan.text(message).show();
        field.addClass('invalid');
    }
    
    //Habilita/deshabilita el botón de guardar según la validez del formulario.
    function checkFormCompletion() {
        let isComplete = true;
        
        $('#registroForm input[required]').each(function() {
            if ($(this).attr('type') === 'checkbox') {
                if (!$(this).is(':checked')) isComplete = false;
            } else {
                if ($(this).val().trim() === '' || $(this).hasClass('invalid')) {
                    isComplete = false;
                }
            }
        });
        
        $('#guardarBtn').prop('disabled', !isComplete);
    }
    
    // Función para enviar datos al WebService
    function enviarDatos(formData) {
        // llamada AJAX al WebService
        console.log('Enviando datos al servidor:', formData);
        
        $.ajax({
            url: "https://localhost:7063/api/Registro",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function (data) {
                console.log("Item creado:", data);
                // Mostrar modal de confirmación
                $('#confirmData').html(`
                    <p><strong>${formData.compania}</strong></p>
                    <p><strong>${formData.nombre}</strong></p>
                    <p><strong>${formData.email}</strong></p>
                    <p>${formData.telefono}</p>
                `);
                $('#confirmModal').show();
            },
            error: function (xhr) {
                // Recorrer cada error con foreach
                let errorHtml = '';
                
                xhr.responseJSON?.forEach(error => {
                    errorHtml += `<p><strong class="text-danger">${error.propertyName}:</strong> ${error.errorMessage}</p>`;
                });

                $('#errorData').html(errorHtml || '<p class="text-danger">Error desconocido</p>');
                $('#errorModal').show();
            }
        });
    }

    // Función para obtener los datos desde el WebService
    function obtenerRegistros() {
        // Obtener datos desde la API
        $.ajax({
            url: "https://localhost:7063/api/Registro",
            type: "GET",
            dataType: "json",
            success: function (data) {
                console.log("Datos recibidos:", data);
                mostrarRegistros(data);
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    }
    
    // Función para mostrar registros guardados
    function mostrarRegistros(registrosGuardados) {
        if (registrosGuardados.length === 0) return;
        
        let html = '<h2>Registros</h2><table>';
        html += '<tr><th>Compañía</th><th>Cédula</th><th>Nombre</th><th>Título</th><th>Correo</th><th>Teléfono</th></tr>';
        
        registrosGuardados.forEach(registro => {
            html += `
                <tr>
                    <td>${registro.compania}</td>
                    <td>${registro.cedula}</td>
                    <td>${registro.nombre}</td>
                    <td>${registro.titulo}</td>
                    <td>${registro.email}</td>
                    <td>${registro.telefono}</td>
                </tr>
            `;
        });
        
        html += '</table>';
        $('.container').html(html + '<button id="nuevoRegistro">Nuevo Registro</button>');
        
        $('#nuevoRegistro').click(function() {
            location.reload();
        });
    }
});