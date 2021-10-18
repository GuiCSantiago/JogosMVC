function apagarJogo(id) {
    if (confirm('Confirma a exclusão do registro?'))
        location.href = 'jogo/Delete?id=' + id;
}