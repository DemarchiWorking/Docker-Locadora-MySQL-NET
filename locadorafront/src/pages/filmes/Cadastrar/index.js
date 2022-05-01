import React from 'react'
import api from '../../../services/api';

import { useNavigate } from 'react-router-dom';
import Header from '../../../components/Header';

import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup";

import './styles.css';

const validationPost = yup.object().shape({
    titulo: yup.string().required("O título é obrigatório").max(200, "O título precisa ter menos de 200 caracteres"),
    classificacaoIndicativa: yup.number().required("a classificacao indicativa é obrigatória"),
    lancamento: yup.bool().required("A data de nascimento é obrigatória")
})

export function CadastrarFilme() {

    let navigate = useNavigate()

    const { register, handleSubmit, formState: { errors } } = useForm({
        resolver: yupResolver(validationPost)
    })

    const addFilme = dados => api.post(`${process.env.REACT_APP_API}`+"/filmes", dados)
    .then((response)=> {
        navigate(`/`);
    })
    .catch((response, err)=> {
        alert("Erro"+err+ JSON.stringify(response));
    });

    return(
        <div>
            <Header />

            <main>

                <div className="card-post" >

                    <h1>Cadastrar Filme</h1>
                    <div className="line-post" ></div>

                    <div className="card-body-post" >

                        <form onSubmit={handleSubmit(addFilme)} >

                            <div className="fields" >
                                <label>Titulo</label>
                                <input type="text" name="titulo" {...register("titulo")} />
                                <p className="error-message">{errors.titulo?.message}</p>
                            </div>

                            <div className="fields" >
                                <label>Classificacao Indicativa</label>
                                <input type="text" name="classificacaoIndicativa" {...register("classificacaoIndicativa")} />
                                <p className="error-message">{errors.classificacaoIndicativa?.message}</p>
                            </div>

                            <div className="fields" >
                                <label>Lançamento</label>
                                <select name="lancamento" {...register("lancamento")} defaultValue="false">
                                    <option value="true">Sim</option>
                                    <option value="false">Não</option>
                                </select>
                                <p className="error-message">{errors.lancamento?.message}</p>
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
