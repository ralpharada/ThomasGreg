class API {
    static urlWeb = 'https://localhost:7249/';
    static urlApi = 'https://localhost:7292/api/';

    static authBearer = async () => {
        let jwt = await this.obterJWT();
        if (!jwt) {
            window.location.href('/login');
        }
        axios.defaults.headers.common['Authorization'] = `Bearer ${jwt}`;
    }
   
    // Função para obter o JWT do cookie
    static obterJWT =async () => {
        const resposta = await axios.get(`${this.urlWeb}obterToken`);
        console.log(resposta)
        return resposta.data;
    }
    

    static abrirModal = () => {
        var modal = document.getElementById('minhaModal');
        modal.classList.remove('hidden');
    }

    static formatarValor = (valor) => {
        const valorFormatado = valor.toLocaleString('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        });

        return valorFormatado;
    }
    static formatarData = (dataString) => {
        const data = new Date(dataString);

        const dia = String(data.getDate()).padStart(2, '0');
        const mes = String(data.getMonth() + 1).padStart(2, '0'); // Meses começam do zero
        const ano = data.getFullYear();
        const horas = String(data.getHours()).padStart(2, '0');
        const minutos = String(data.getMinutes()).padStart(2, '0');
        const segundos = String(data.getSeconds()).padStart(2, '0');

        const dataFormatada = `${dia}/${mes}/${ano} ${horas}:${minutos}:${segundos}`;

        return dataFormatada;
    }
    //static sair = () => {
    //    this.limparCookies();
    //    window.location.reload();
    //}
    //static limparCookies = () => {
    //    const cookies = document.cookie.split(';');
    //    for (let i = 0; i < cookies.length; i++) {
    //        const cookie = cookies[i];
    //        const igualPosicao = cookie.indexOf('=');
    //        const nome = igualPosicao > -1 ? cookie.substr(0, igualPosicao) : cookie;
    //        document.cookie = `${nome}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
    //    }
    //}
}
//document.addEventListener('DOMContentLoaded', async function () {
//    const pathname = window.location.pathname;
//    const verificarJWTValido = await API.verificarJWTValido();
//    if (verificarJWTValido) {
//        if (pathname == '/login')
//            window.location.href = '/';
//    } else {
//        if (pathname != '/login')
//            window.location.href = '/login';
//    }
//    var overlay = document.querySelector('.overlay');
//    overlay.style.display = 'none';

// //   const botao = document.getElementById('sair');
//    if (botao) {
//        botao.addEventListener('click', function () {
//            API.sair();
//        });
//    }
//});