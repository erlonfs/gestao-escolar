USE GestaoEscolar;
GO

--AlunoSituacao
INSERT INTO GES.AlunoSituacao(Id, Nome) VALUES(1, 'Não Matriculado');
INSERT INTO GES.AlunoSituacao(Id, Nome) VALUES(2, 'Matriculado');
INSERT INTO GES.AlunoSituacao(Id, Nome) VALUES(3, 'Transferido');
INSERT INTO GES.AlunoSituacao(Id, Nome) VALUES(4, 'Expulso');

--SalaTurno
INSERT INTO GES.SalaTurno(Id, Nome) VALUES(1, 'Matutino');
INSERT INTO GES.SalaTurno(Id, Nome) VALUES(2, 'Vespertino');
INSERT INTO GES.SalaTurno(Id, Nome) VALUES(3, 'Noturno');