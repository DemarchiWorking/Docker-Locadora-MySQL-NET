import { BrowserRouter, Routes, Route } from 'react-router-dom';

import { Inicial } from './pages/inicial'
import { CadastrarCliente } from './pages/clientes/Cadastrar';
import { ListarCliente } from './pages/clientes/Listar';
import { EditarCliente } from './pages/clientes/Editar';

import { CadastrarFilme } from './pages/filmes/Cadastrar';
import { ListarFilme } from './pages/filmes/Listar';
import { EditarFilme } from './pages/filmes/Editar';

import { CadastrarLocacao } from './pages/locacoes/Cadastrar';
import { ListarLocacao } from './pages/locacoes/Listar';
import { EditarLocacao } from './pages/locacoes/Editar';

import { Funcionalidades } from './pages/funcionalidades/index';
import { Atrasados, MenosAlugados, NaoAlugados, TopAlugados, SegundoMelhorCliente } from './pages/funcionalidades/resultado';

function RoutesApp(){

return (
    <BrowserRouter>
        <Routes>
            <Route path='/' element={ <Inicial/> }/>
            <Route path="/clientes/Cadastrar" element={ <CadastrarCliente/>}/>
            <Route path="/clientes/Editar" element={ <EditarCliente/>}/>
            <Route path="/clientes/Editar/:idCliente" element={ <EditarCliente/>}/>
            <Route path="/clientes/Listar" element={ <ListarCliente/>}/>

            
            <Route path="/filmes/Cadastrar" element={ <CadastrarFilme/>}/>
            <Route path="/filmes/Editar" element={ <EditarFilme/>}/>
            <Route path="/filmes/Editar/:idFilme" element={ <EditarFilme/>}/>
            <Route path="/filmes/Listar" element={ <ListarFilme/>}/>

            <Route path="/locacoes/Cadastrar" element={ <CadastrarLocacao/>}/>
            <Route path="/locacoes/Editar" element={ <EditarLocacao/>}/>
            <Route path="/locacoes/Editar/:idLocacao" element={ <EditarLocacao/>}/>
            <Route path="/locacoes/Listar" element={ <ListarLocacao/>}/>

            <Route path="/funcionalidades" element={ <Funcionalidades/>}/>
            <Route path="/funcionalidades/atrasados" element={ <Atrasados/>}/>
            <Route path="/funcionalidades/nao-alugados" element={ <NaoAlugados/>}/>
            <Route path="/funcionalidades/top-alugados" element={ <TopAlugados/>}/>
            <Route path="/funcionalidades/menos-alugados" element={ <MenosAlugados/>}/>      
            <Route path="/funcionalidades/segundo-melhor-cliente" element={ <SegundoMelhorCliente/>}/>      

            
            
        </Routes>
    </BrowserRouter>
)
    
}
export default RoutesApp;