import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { Novosti } from "./Novost";
import DodajNovost from "./DodajNovost";
import {
  TextField,
  Box,
  Button,
  Typography,
  Card,
  CardActions,
  CardContent,
  CardMedia,
} from "@mui/material";
import Zivotinja from "./Zivotinja";
export function Post() {
  const [post, setPost] = useState(null);
  const [loading, setLoading] = useState(false);
  let navigate = useNavigate();
  const location = useLocation();
  useEffect(() => {
    console.log(location);
    setPost(location.state.post);
  }, []);
  return (
    <>
      {(post && (

        <Box sx={{ display: 'flex', flexDirection: 'row', justifyContent:'space-around', flexWrap: 'wrap'}}>
        
        <Card key={post.id}>
        
        <CardActions>
          <Button size="small" variant="contained" color="secondary"onClick={() => {
                    navigate("/doniraj", { state: { id_posta: post.id} });
                  }}>Doniraj</Button>

				  {/*<Button size="small">Udomi</Button>*/}
        </CardActions>
        <CardMedia
                height="140"
                alt="stock"
                image={post.slike.length == 0 ? "imgs/stockphoto.jpg" : post.slike[0]}
                title="slika"
                component="img"
              />
        <CardContent>
          <Typography gutterBottom variant="h5" component="div">
            {post.naziv}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            {post.opis}
          </Typography>
        </CardContent>
        {post&&<Zivotinja id_posta={post.id} />}
      </Card>
      
        <Box >
      <Typography variant="subtitle2" component="div">
            NOVOSTI
      </Typography>
      { post && <Novosti novost={post.id} loading={loading} setLoading={setLoading}></Novosti>} </Box>
      <Box>
      <DodajNovost id_slucaja={post.id} loading={loading} setLoading={setLoading}/>
      </Box>
      </Box>
      )) || <>ne ucitava post</>}
    </>
  );
}
