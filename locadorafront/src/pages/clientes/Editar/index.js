import React, { useEffect } from 'react'
import api from '../../../services/api';

import { useNavigate, useParams } from 'react-router-dom';
import Header from '../../../components/Header';

import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup";

import './styles.css';

const validationPost = yup.object().shape({
    nome: yup.string().required("O nome é obrigatório").max(200, "O nome precisa ter menos de 200 caracteres"),
    cpf: yup.string().required("O Cpf é obrigatório").max(11, "O CPF precisa ter menos de 12 caracteres"),
    dataNascimento: yup.string().required("A data de nascimento é obrigatória")
})

export function EditarCliente() {

    const { idCliente } = useParams();
    let navigate = useNavigate();
    
    const { register, handleSubmit, formState: { errors }, reset } =  useForm({
        defaultValues: {
            nome: "",
            cpf: ""
         },
        resolver: yupResolver(validationPost)
    })

    const addCliente = dados => api.put(`${process.env.REACT_APP_API}/clientes/${idCliente}`, dados)
    .then((response)=> {
        alert(JSON.stringify(response));
        navigate(`/`);
    })
    .catch((response, err)=> {
        alert("Erro"+err+ JSON.stringify(response));
    });

    
    function resetar (nome, cpf){
        reset({
            nome: nome,
            cpf:  cpf      
          }, { keepDefaultValues: true });
    }

      useEffect(()=> {
        api.get(`${process.env.REACT_APP_API}/clientes/${idCliente}`)
            .then((response) => {
                alert(`${process.env.REACT_APP_API}/clientes/${idCliente}`)
               resetar(response.data.list[0].nome, response.data.list[0].cpf)
            })
            .catch((err) => {
                alert("Erro:"+ err + " --- "+`${process.env.REACT_APP_API}/clientes/${idCliente}`)
            })
    },[])
    return(
        <div>
            <Header />

            <main>

                <div className="card-post" >

                    <h1>Editar Cliente</h1>
                    <div className="line-post" ></div>

                    <div className="card-body-post" >

                        <form onSubmit={handleSubmit(addCliente)} >

                            <div className="fields" >
                                <label>Nome</label>
                                <input type="text" name="nome" {...register("nome")} />
                                <p className="error-message">{errors.nome?.message}</p>
                            </div>

                            <div className="fields" >
                                <label>CPF</label>
                                <input type="text" name="cpf" {...register("cpf")} />
                                <p className="error-message">{errors.cpf?.message}</p>
                            </div>

                            <div className="fields" >
                                <label>Data de Nascimento</label>
                                <input type="date" name="dataNascimento" {...register("dataNascimento")}/>
                                <p className="error-message">{errors.dataNascimento?.message}</p>
                            </div>

                            <div className="btn-cadastrar" >
                                <button type="submit" >Enviar</button>
                            </div>

                        </form>

                    </div>

                </div>

            </main>

        </div>
    )
}
