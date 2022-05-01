import React from 'react';
import { useNavigate } from 'react-router-dom';
import Header from '../../../components/Header';
import { Form, Input, Button, message } from 'antd';
import * as yup from "yup";
import api from '../../../services/api';
import './styles.css';
const { Item } = Form;
    


// COM ANTD 
const validacaoCadastro = yup.object().shape({
    nome: yup.string().required("O título é obrigatório").max(200, "O nome precisa ter menosde 200 caracteres"),
    cpf: yup.string().required("A descrição é obrigatório").max(12, "O cpf precisa ter menosde 150 caracteres"),
    dataNascimento: yup.date().required("A data é obrigatória")
})
export function CadastrarCliente() {

    let history = useNavigate()

    
    const cadastroSucesso = (dados) =>{
       
        api.post(`${process.env.REACT_APP_API}`+"/clientes", dados)
            .then((response)=> {
                alert(JSON.stringify(response));
                history.apply("/")
            })
            .catch((response, err)=> {
                alert("Erro"+err+ JSON.stringify(response));
            });
    }
    const cadastroFailed = (error) =>{
        console.log("Erro :"+error);
    }

    return(
        <div>
            <Header />

            <main>

                <div className="card-post" >

                    <h1>Cadastrar Cliente</h1>
                    <div className="line-post" ></div>

                    <div className="card-body-post" >

                        
                <Form name='cadastrarCliente' initialValues={{
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

            </main>

        </div>
    )
}
