import { BrowserRouter, Routes, Route } from 'react-router-dom';

import { Inicial } from './pages/inicial'
import { CadastroCliente } from './pages/cliente/Cadastro';
import { ListarCliente } from './pages/cliente/Listar';


function RoutesApp(){

return (
    <BrowserRouter>
        <Routes>
            <Route path='/' element={ <Inicial/> }/>
            <Route path="/clientes/Cadastrar" element={ <CadastroCliente/>}/>
            <Route path="/clientes/Listar" element={ <ListarCliente/>}/>
        </Routes>
    </BrowserRouter>
)
    
}
export default RoutesApp;