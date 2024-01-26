import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { Novosti } from "./Novost";
import DodajNovost from "./DodajNovost";
import { TextField, Box, Button, Typography, Card, CardActions, CardContent } from "@mui/material";
import Zivotinja from "./Zivotinja";
export function Post() {
  const [post, setPost] = useState(null);
  let navigate = useNavigate();
  const location = useLocation();
  useEffect(() => {
    console.log(location);
    setPost(location.state.post);
  }, []);
  return (
    <>
      {(post && (
        <Box sx={{ display: 'flex', flexDirection: 'row' }}>
        
        <Card key={post.id}>
        
        <CardActions>
          <Button size="small" onClick={() => {
                    navigate("/doniraj", { state: { id_posta: post.id } });
                  }}>Doniraj</Button>

          <Button size="small">Udomi</Button>
        </CardActions>
        {/* <CardMedia
          sx={{ height: 140 }}
          image="https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.istockphoto.com%2Fphotos%2Fplaceholder-image&psig=AOvVaw3iu14a8dxZufPTObHcDmUR&ust=1705892562250000&source=images&cd=vfe&ved=0CBMQjRxqFwoTCKDv5ZO_7YMDFQAAAAAdAAAAABAE"
          title="slika"
        /> */}
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
      
      <div>
      <Typography variant="subtitle2" component="div">
            NOVOSTI
          </Typography>
      { post && <Novosti novost={post.id}></Novosti>} </div>
      <DodajNovost/>
      </Box>
      )) || <>ne ucitava post</>}
    </>
  );
}
