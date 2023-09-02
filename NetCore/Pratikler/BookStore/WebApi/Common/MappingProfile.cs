using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateBookCommand
            CreateMap<CreateBookModel, Book>();//CreateBookModel, Book Modeline haritalanabilir(mapping).
            //GetBookDetailQuery
            //CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())); Genre Include dan önce geçerli
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));
            //GetBooksQuery
            // CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())); Genre Include dan önce
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));

            //GetGenresQuery
            CreateMap<Genre, GenresViewModel>();
            //GetGenreDetailQuery
            CreateMap<Genre, GenreDetailViewModel>();

            //CreateAuthorCommand
            CreateMap<CreateAuthorModel, Author>();
            //GetAuthorsQuery
            CreateMap<Author, AuthorsViewModel>();
            //GetAuthorDetailQuery
            CreateMap<Author, AuthorDetailViewModel>();

            //CreateUser
            CreateMap<CreateUserModel, User>();
        }
    }
}