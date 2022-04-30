import { Link } from 'react-router-dom';

export function Inicial() {
    return (
        <div>
            <h1>Home</h1>
            <p><span> Bem-Vindo </span></p> 

            <p><Link to="/clientes/cadastrar">Cadastrar Cliente </Link></p> 
            <Link to="/clientes/listar">Listar Cliente </Link>
            
        </div>
    )
}
