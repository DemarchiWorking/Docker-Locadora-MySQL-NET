import React, { useEffect } from 'react'
import api from '../../../services/api';

import { useNavigate, useParams } from 'react-router-dom';
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

export function EditarLocacao() {

    const { idLocacao } = useParams();
    let navigate = useNavigate();
    
    const { register, handleSubmit, formState: { errors }, reset } =  useForm({
        defaultValues: {
            idCliente: "",
            idFilme: "",
            dataLocacao: "",
            dataDevolucao: ""
         },
        resolver: yupResolver(validationPost)
    })

    const addLocacao = dados => api.put(`${process.env.REACT_APP_API}/locacoes/${idLocacao}`, dados)
    .then((response)=> {
        alert(JSON.stringify(response));
        navigate(`/`);
    })
    .catch((response, err)=> {
        alert("Erro"+err+ JSON.stringify(response));
    });

    
    function resetar (idCliente, idFilme, dataLocacao, dataDevolucao){
        reset({
            idCliente: idCliente,
            idFilme:  idFilme,
            dataLocacao: dataLocacao,
            dataDevolucao: dataDevolucao
          }, { keepDefaultValues: true });
    }

      useEffect(()=> {
        api.get(`${process.env.REACT_APP_API}/locacoes/${idLocacao}`)
            .then((response) => {
                alert(`${process.env.REACT_APP_API}/locacoes/${idLocacao}`)
               resetar(response.data.list[0].idCliente, response.data.list[0].idFilme, response.data.list[0].dataLocacao, response.data.list[0].dataDevolucao)
            })
            .catch((err) => {
                alert("Erro:"+ err + " --- "+`${process.env.REACT_APP_API}/locacoes/${idLocacao}`)
            })
    },[])
    return(
        <div>
            <Header />

            <main>

                <div className="card-post" >

                    <h1>Editar Locações</h1>
                    <div className="line-post" ></div>

                    <div className="card-body-post" >

                        <form onSubmit={handleSubmit(addLocacao)} >

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
