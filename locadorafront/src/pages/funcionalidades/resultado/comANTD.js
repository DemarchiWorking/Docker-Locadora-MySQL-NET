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
export function Resultado() {
    const [clientes, setClientes] = useState([]);
    const data = [
        'Racing car sprays burning fuel into crowd.',
        'Japanese princess to wed commoner.',
        'Australian walks 100km after outback crash.',
        'Man charged over missing wedding girl.',
        'Los Angeles battles huge wildfires.',
      ];

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
         <Divider orientation="left"> Lista de Atrasados </Divider>
    <List
      size="large"
      header={<div>Header</div>}
      footer={<div>Footer</div>}
      bordered
      dataSource={clientes}
      renderItem={item => <List.Item>{"Id Filme: "+ item.idFilme +" Titulo :"+item.titulo+"Nome: " + item.nome+"|  CPF : "+item.cpf+ " | "}</List.Item>}
    />
        <Header/>
        <p><Link to="/">Home </Link></p> 
            <div className='containerPrincipal'>
                    <div className='containerSecundario'>

                   
                <table>
                    <thead>
                        <tr>
                            <th style={{ width: '170px', textAlign: 'center' }}>Título</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>CPF</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Data Nascimento</th>
                            <th style={{ width: '170px', textAlign: 'center' }}> Ações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {clientes?.map((cliente, index) => {
                            return (
                                <tr key={index} style={{ flex: 1, flexDirection: 'row', backgroundColor: index % 2 === 0 ? '#ccc' : '#fff' }}>

                                    <td style={{ width: '170px', textAlign: 'center' }}> {cliente.nome}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.cpf}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.dataNascimento}</td>
                                    <td style={{ display: 'flex', justifyContent: 'space-around' }}>
                                        <button> Editar </button>
                                        <button> Excluir </button>
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
