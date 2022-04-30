//import { useState } from 'react';
import { Form, Input, Button, message } from 'antd';
import './styles.css';
import api from 'axios';
import msgSucesso from '../../../config/global';
import msgErro from '../../../config/global';
import { Link } from 'react-router-dom';

export function CadastroCliente() {
    const { Item } = Form;
    
    //async function cds(){         }
    const cadastroSucesso = (dados) =>{
        alert(msgSucesso + JSON.stringify(dados));
       
        api.post(`${process.env.REACT_APP_API}`+"/clientes", dados)
            .then((response)=> {
                alert(msgSucesso+ JSON.stringify(response));
            })
            .catch((err)=> {
                alert(msgErro+err);
            });
    }
    const cadastroFailed = (error) =>{
        console.log(msgErro+error);
    }
    
  return (
    <div>
        <p><Link to="/">Home </Link></p> 
       <div className="containerPrincipal">
            <div className="containerSecundario">

                <Form name='cadastroCliente' initialValues={{
                    recordar:true
                }}
                onFinish={cadastroSucesso}
                onFinishFailed={cadastroFailed}>

                    <Item label="Nome"
                            name="nome"
                            rules={[{
                                required: true,
                                message: "Digite um nome."
                            }]}> 
                        <Input></Input>
                    </Item>

                    <Item label="Cpf"
                            name="cpf"
                            rules={[{
                                required: true,
                                message: "Digite um Documento."
                            }]}> 
                        <Input></Input>
                    </Item>

                    <Item label="Nascimento"
                            name="dataNascimento"
                            rules={[{
                                required: true,
                                message: "Digite uma Data de Nascimento."
                            }]}> 
                        <Input type="date"></Input>
                    </Item>

                    <Item>
                        <Button type='primary' htmlType='submit'>Cadastrar </Button>
                    </Item>
                </Form>
            </div>

       </div>
    </div>
  );
}
