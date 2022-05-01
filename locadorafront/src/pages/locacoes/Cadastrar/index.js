import React from 'react'
import api from '../../../services/api';

import { useNavigate } from 'react-router-dom';
import Header from '../../../components/Header';

import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup";

import './styles.css';

const validationPost = yup.object().shape({
    idCliente: yup.number().required("Id do cliente é obrigatório"),
    idFilme: yup.number().required("Id do filme é obrigatório"),
    dataLocacao: yup.date().required("A data da locação é obrigatória"),
    dataDevolucao: yup.date() .nullable()
    .transform((curr, orig) => orig === '' ? null : curr)
})

export function CadastrarLocacao() {

    let navigate = useNavigate()

    
    const { register, handleSubmit, formState: { errors } } = useForm({
        resolver: yupResolver(validationPost)
   
    })

    const addLocacao = dados => api.post(`${process.env.REACT_APP_API}`+"/locacoes", dados)
    .then((response)=> {
        alert(response.data.title);
        navigate(`/`);
        
    })
    .catch((response, err)=> {
        alert("Erro :"+err+ JSON.stringify(response));
    });

       

    return(
        <div>
            <Header />

            <main>

                <div className="card-post" >

                    <h1>Cadastrar Locação</h1>
                    <div className="line-post" ></div>

                    <div className="card-body-post" >

                        <form onSubmit={handleSubmit(addLocacao)} >

                            
                            <div className="fields" >
                                <label> Id Cliente</label>
                                <input type="text" name="idCliente" {...register("idCliente")} />
                                <p className="error-message">{errors.idCliente?.message}</p>
                            </div>
                            <div className="fields" >
                                <label>Id Filme</label>
                                <input type="text" name="idFilme" {...register("idFilme")} />
                                <p className="error-message">{errors.classificacaoIndicativa?.message}</p>
                            </div>

                            <div className="fields" >
                                <label> Data de Locação </label>
                                <input type="datetime-local" name="dataLocacao" {...register("dataLocacao")}/>
                                <p className="error-message">{errors.dataLocacao?.message}</p>
                            </div>

                            <div className="fields" >
                                <label>Data de Devolução</label>
                                <input type="datetime-local"  name="dataDevolucao" {...register("dataDevolucao")}/>
                                <p className="error-message">{errors.dataDevolucao?.message}</p>
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
