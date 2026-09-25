$(function(){
    $(document).on('submit', 'form.swal-delete-form',function(e){
        e.preventDefault();
        var form = this;
        Swal.fire({
            title: '¿Desea eliminar esto?',
            text: 'Esta acción no se puede deshacer',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Sí',
            cancelButtonText: 'No'
        })
        .then(function(result){
            if(result.isConfirmed){
                form.submit();
            }
        })
    });

    $(document).on('submit', 'form.swal-save-form', function(e) {
        e.preventDefault();
        var form = this;
        Swal.fire({
            title: '¿Desea editar esto?',
            text: 'Esta acción no se puede deshacer',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sí',
            cancelButtonText: 'No'
        })
            .then(function(result) {
                if (result.isConfirmed) {
                    form.submit();
                }
            })
    });

});