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
    entregue: yup.bool(),
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
            dataDevolucao: "",
            entregue: false
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

    
    function resetar(idCliente, idFilme, dataLocacao, dataDevolucao,entregue){
        

        reset({
            idCliente: idCliente,
            idFilme:  idFilme,
            dataLocacao: dataLocacao,            
            dataDevolucao: dataDevolucao,
          }, { keepDefaultValues: true });
    }

      useEffect(()=> {
        api.get(`${process.env.REACT_APP_API}/locacoes/${idLocacao}`)
            .then((response) => {
               resetar(response.data.list[0].ID_CLIENTE, response.data.list[0].ID_FILME, response.data.list[0].DATA_LOCACAO, response.data.list[0].DATA_DEVOLUCAO)
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
                            <div>
                                <input  type="checkbox" id="entregue" name="entregue" {...register("entregue")}/>
                                <label for="horns" style={{textAlign:'center'}}> Não Devolvido</label>
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
