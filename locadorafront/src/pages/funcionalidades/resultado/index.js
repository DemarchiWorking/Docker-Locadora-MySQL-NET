import { Link } from 'react-router-dom';
import Header from '../../../components/Header';
import './styles.css';
import React, { useState, useEffect } from 'react';
import api from '../../../services/api';
import { List, Typography, Divider } from 'antd';

//import { ClientesAtrasados } from '../index/index'
/*
export function RelatorioClientesDevolucaoAtrasada(){
       
    const [clientes, setClientes] = useState([]);
    return ""
}*/

export function ListarFilme() {

    
    
    const [filmes, setFilmes] = useState([]);
    useEffect(() => {
        api.get(`${process.env.REACT_APP_API}/filmes`)
            .then((response) => {
                setFilmes(response.data.list);
                console.log(response.data.list);

            })
            .catch((err) => {
                alert("Erro :"+ err);
                console.log('errado');
            })
    }, []);

    function deletarFilme(idFilme){
        
        api.delete(`${process.env.REACT_APP_API}/filmes/${idFilme}`)
            .then((response) =>{
                alert(JSON.stringify(response));
                setFilmes(filmes.filter(filme => filme.ID_FILME !== idFilme ))
            })
            .catch((err) =>{
                
                alert(`${process.env.REACT_APP_API}/filmes/${idFilme}`);
                alert("Erro: "+ err);
            })
        
    }

    return (
        <div>
               <Header />
            <h1>Filmes</h1>

            <div></div>

            <main>
                <div className="cards">
                    <div className="card">

                    {filmes?.map((filme, index) => {
                            return (
                                <div key={index}>
                                <header>
                                    {
                                    }
                                    <h2> {filme.TITULO} </h2>
                                </header>
                                    <div className="linha"> </div>      
        
                                    <p>Id :{filme.ID_FILME}</p>
                                    <p>Classificação Indicativa :{filme.CLASSIFICACAO_INDICATIVA}</p>
                                    <p>Lançamento: {(filme.LANCAMENTO==0)?'nao':'sim'}</p>
        
        
                                    <div className='btns'>
                                        <div className='btn-editar'>
                                            <Link to={{ pathname: `/filmes/Editar/${filme.ID_FILME}`}}>
                                                <button>Editar</button>
                                            </Link>
                                        </div>
                                        <div className='btn-deletar'>
                                            <button onClick={ 
                                                () => deletarFilme(filme.ID_FILME)
                                                 }>Deletar</button>
                                        </div>
        
                                    </div>
                                </div>
                            )
                        })}
                        
                    
                    </div>
                </div>
            </main>

            
        </div>
    )
}
export function NaoAlugados() {
    const [filmes, setFilmes] = useState([]);
   
    function isTrue(lancamento) {
        if(lancamento == true){
            return "Lançamento"
        }else{
            return "Comum"
        }
    }

    alert(`${process.env.REACT_APP_API}/funcionalidades/nao-alugados`);
    useEffect(() => {
        
        api.get(`${process.env.REACT_APP_API}/funcionalidades/nao-alugados`)
            .then((response) => {
                setFilmes(response.data.list);
                console.log(response.data);
                alert("clie: "+filmes)
            })
            .catch((err) => {
                alert("Erro :" + err);
                console.log('errado');
            })
    }, []);
    
for(let i = 0; i < 1; i++) {                              
    return (
        <>
    
        <Header/>
        <p><Link to="/">Home </Link></p> 
            <div className='containerPrincipal'>
                    <div className='containerSecundario'>

                   
                <table>
                    <thead>
                        <tr>
                            <th style={{ width: '170px', textAlign: 'center' }}>Id Filme </th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Título </th>  
                            <th style={{ width: '170px', textAlign: 'center' }}> Classificação Indicativa </th>      
                            <th style={{ width: '170px', textAlign: 'center' }}> Lançamento </th>
                        </tr>
                    </thead>
                    <tbody>
                        {filmes?.map((filme, index) => {
                            return (
                                <tr key={index} style={{ height: '100px', flex: 1, flexDirection: 'row', backgroundColor: index % 2 === 0 ? '#29EBAA' : '#fff' }}>

                                    <td style={{ width: '170px', textAlign: 'center' }}> {filme.idFilme}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{filme.titulo}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{filme.classificacaoIndicativa}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{isTrue(filme.lancamento)}</td>
                                    <td style={{ display: 'flex', justifyContent: 'space-around' }}>
                                    </td>
                                </tr>


                            )
                        })}

                    </tbody>
                </table>
            </div> 
            </div>
                       

        </>
    );       
} 
}

export function Atrasados() {
    const [clientes, setClientes] = useState([]);

    function aberto(dataDevolucao) {
        if(dataDevolucao == '0001-01-01T00:00:00'){
            return "Em Aberto"
        }else{
            return dataDevolucao
        }
    }

    alert(`${process.env.REACT_APP_API}/funcionalidades/atrasados`);
    useEffect(() => {
        
        api.get(`${process.env.REACT_APP_API}/funcionalidades/atrasados`)
            .then((response) => {
                setClientes(response.data.list);
                console.log(response.data);
                alert("clie: "+clientes)
            })
            .catch((err) => {
                alert("Erro :" + err);
                console.log('errado');
            })
    }, []);
    
for(let i = 0; i < 1; i++) {                              
    return (
        <>
    
        <Header/>
        <p><Link to="/">Home </Link></p> 
            <div className='containerPrincipal'>
                    <div className='containerSecundario'>
                   
                   
                <table>
                    <thead>
                        <tr>
                            <th style={{ width: '170px', textAlign: 'center' }}>Id Locação </th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Id Filme</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Título</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Id Cliente</th>                            
                            <th style={{ width: '170px', textAlign: 'center' }}>Nome</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>CPF</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Dias Atraso</th>
                            <th style={{ width: '170px', textAlign: 'center' }}> Locação</th>
                            <th style={{ width: '170px', textAlign: 'center' }}> Devolução</th>
                        </tr>
                    </thead>
                    <tbody>
                        {clientes?.map((cliente, index) => {
                            return (
                                <tr key={index} style={{ height: '100px', flex: 1, flexDirection: 'row', backgroundColor: index % 2 === 0 ? '#29EBAA' : '#fff' }}>

                                    <td style={{ width: '170px', textAlign: 'center' }}> {cliente.idLocacao}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}> {cliente.idFilme}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.titulo}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.idCliente}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.nome}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.cpf}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.tempoAtraso}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.dataLocacao}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{aberto(cliente.dataDevolucao)}</td>
                                    <td style={{ display: 'flex', justifyContent: 'space-around' }}>
                                    </td>
                                </tr>


                            )
                        })}

                    </tbody>
                </table>
            </div> 
            <div style={{color:'red'}}> * Prazo de Entrega:
                            - Lançamentos : 3 Dias 
                            - Comuns : 2 Dias
                    </div>
            </div>

           
        </>
    );       
} 
}



export function TopAlugados() {
    const [filmes, setFilmes] = useState([]);
   

    useEffect(() => {
        
        api.get(`${process.env.REACT_APP_API}/funcionalidades/top-alugados`)
            .then((response) => {
                setFilmes(response.data.list);
                console.log(response.data);
            })
            .catch((err) => {
                alert("Erro :" + err);
                console.log('errado');
            })
    }, []);

for(let i = 0; i < 1; i++) {                              
    return (
        <>
    
        <Header/>
        <p><Link to="/">Home </Link></p> 
            <div className='containerPrincipal'>
                    <div className='containerSecundario'>

                   
                <table>
                    <thead>
                        <tr>
                            <th> Rank </th>
                            <th style={{ width: '170px', textAlign: 'center' }}> Alugado(x) </th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Id Filme</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Título</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>classificacaoIndicativa</th>   
                            <th style={{ width: '170px', textAlign: 'center' }}>lancamento</th>   
                        </tr>
                    </thead>
                    <tbody>
                        {filmes?.map((filme, index) => {
                            return (
                                <tr key={index} style={{ height: '100px', flex: 1, flexDirection: 'row', backgroundColor: index % 2 === 0 ? '#29EBAA' : '#fff' }}>
                                    <td> {index+1} </td>                                    
                                    <td style={{ width: '170px', textAlign: 'center' }}> {filme.vendas}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}> {filme.idFilme}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{filme.titulo}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{filme.classificacaoIndicativa}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{ filme.lancamento == 0 ? "false" : "true"}</td>
                                    <td style={{ display: 'flex', justifyContent: 'space-around' }}>
                                    </td>
                                </tr>


                            )
                        })}

                    </tbody>
                </table>
            </div> 
            </div>


        </>
    );       
} 
}


export function MenosAlugados() {
    const [filmes, setFilmes] = useState([]);
   

    useEffect(() => {
        alert(`${process.env.REACT_APP_API}/funcionalidades/menos-alugados`)
        api.get(`${process.env.REACT_APP_API}/funcionalidades/menos-alugados`)
            .then((response) => {
                setFilmes(response.data.list);
                console.log(response.data);
            })
            .catch((err) => {
                alert("Erro :" + err);
                console.log('errado');
            })
    }, []);

for(let i = 0; i < 1; i++) {                              
    return (
        <>
    
        <Header/>
        <p><Link to="/">Home </Link></p> 
            <div className='containerPrincipal'>
                    <div className='containerSecundario'>

                   
                <table>
                    <thead>
                        <tr>
                            <th style={{ width: '170px', textAlign: 'center' }}>Alugado(x)</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Id Filme</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Título</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>classificacaoIndicativa</th>   
                            <th style={{ width: '170px', textAlign: 'center' }}>lancamento</th>   
                        </tr>
                    </thead>
                    <tbody>
                        {filmes?.map((filme, index) => {
                            return (
                                <tr key={index} style={{ height: '100px', flex: 1, flexDirection: 'row', backgroundColor: index % 2 === 0 ? '#29EBAA' : '#fff' }}>

                                    <td style={{ width: '170px', textAlign: 'center' }}> {filme.vendas}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}> {filme.idFilme}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{filme.titulo}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{filme.classificacaoIndicativa}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{ filme.lancamento == 0 ? "false" : "true"}</td>
                                    <td style={{ display: 'flex', justifyContent: 'space-around' }}>
                                    </td>
                                </tr>


                            )
                        })}

                    </tbody>
                </table>
            </div> 
            </div>


        </>
    );       
} 
}

export function SegundoMelhorCliente() {
    const [clientes, setCliente] = useState([]);

    
    useEffect(() => {
        alert(`${process.env.REACT_APP_API}/funcionalidades/segundo-melhor-cliente`)
        api.get(`${process.env.REACT_APP_API}/funcionalidades/segundo-melhor-cliente`)
            .then((response) => {
                setCliente(response.data.list);
                console.log(response.data);
            })
            .catch((err) => {
                alert("Erro :" + err);
                console.log('errado');
            })
    }, []);

for(let i = 0; i < 1; i++) {                              
    return (
        <>
    
        <Header/>
        <p><Link to="/">Home </Link></p> 
            <div className='containerPrincipal'>
                    <div className='containerSecundario'>

                   
                <table>
                    <thead>
                        <tr>
                            <th style={{ width: '170px', textAlign: 'center' }}>Id Cliente</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Nome</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>CPF</th>   
                            <th style={{ width: '170px', textAlign: 'center' }}>Data de Nascimento</th>  
                            <th style={{ width: '170px', textAlign: 'center' }}> Alugados(x)</th>   
                        </tr>
                    </thead>
                    <tbody>
                        {clientes?.map((cliente, index) => {
                            return (
                                <tr key={index} style={{ height: '100px', flex: 1, flexDirection: 'row', backgroundColor: index % 2 === 0 ? '#29EBAA' : '#fff' }}>
                                   
                                    <td style={{ width: '170px', textAlign: 'center' }}> {cliente.idCliente}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}> {cliente.nome}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.cpf}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.dataNascimento}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{ cliente.vendas}</td>
                                    <td style={{ display: 'flex', justifyContent: 'space-around' }}>
                                    </td>
                                </tr>


                            )
                        })}

                    </tbody>
                </table>
            </div> 
            </div>


        </>
    );       
} 
}


