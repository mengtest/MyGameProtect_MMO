-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local Role_pb = require("protopb/Role_pb")
module('Login_pb')


local LOGINGAMEREQ = protobuf.Descriptor();
local LOGINGAMEREQ_USERNAME_FIELD = protobuf.FieldDescriptor();
local LOGINGAMEREQ_PASSWORD_FIELD = protobuf.FieldDescriptor();
local LOGINGAMERES = protobuf.Descriptor();
local LOGINGAMERES_RESULT_FIELD = protobuf.FieldDescriptor();
local LOGINGAMERES_ROLEDATA_FIELD = protobuf.FieldDescriptor();
local CREATEROLEREQ = protobuf.Descriptor();
local CREATEROLEREQ_USERNAME_FIELD = protobuf.FieldDescriptor();
local CREATEROLEREQ_PASSWORD_FIELD = protobuf.FieldDescriptor();
local CREATEROLERES = protobuf.Descriptor();
local CREATEROLERES_RESULT_FIELD = protobuf.FieldDescriptor();
local MESSAGEERRORRES = protobuf.Descriptor();
local MESSAGEERRORRES_RESULT_FIELD = protobuf.FieldDescriptor();

LOGINGAMEREQ_USERNAME_FIELD.name = "username"
LOGINGAMEREQ_USERNAME_FIELD.full_name = ".com.kz.game.message.proto.LoginGameReq.username"
LOGINGAMEREQ_USERNAME_FIELD.number = 1
LOGINGAMEREQ_USERNAME_FIELD.index = 0
LOGINGAMEREQ_USERNAME_FIELD.label = 2
LOGINGAMEREQ_USERNAME_FIELD.has_default_value = false
LOGINGAMEREQ_USERNAME_FIELD.default_value = ""
LOGINGAMEREQ_USERNAME_FIELD.type = 9
LOGINGAMEREQ_USERNAME_FIELD.cpp_type = 9

LOGINGAMEREQ_PASSWORD_FIELD.name = "password"
LOGINGAMEREQ_PASSWORD_FIELD.full_name = ".com.kz.game.message.proto.LoginGameReq.password"
LOGINGAMEREQ_PASSWORD_FIELD.number = 2
LOGINGAMEREQ_PASSWORD_FIELD.index = 1
LOGINGAMEREQ_PASSWORD_FIELD.label = 2
LOGINGAMEREQ_PASSWORD_FIELD.has_default_value = false
LOGINGAMEREQ_PASSWORD_FIELD.default_value = ""
LOGINGAMEREQ_PASSWORD_FIELD.type = 9
LOGINGAMEREQ_PASSWORD_FIELD.cpp_type = 9

LOGINGAMEREQ.name = "LoginGameReq"
LOGINGAMEREQ.full_name = ".com.kz.game.message.proto.LoginGameReq"
LOGINGAMEREQ.nested_types = {}
LOGINGAMEREQ.enum_types = {}
LOGINGAMEREQ.fields = {LOGINGAMEREQ_USERNAME_FIELD, LOGINGAMEREQ_PASSWORD_FIELD}
LOGINGAMEREQ.is_extendable = false
LOGINGAMEREQ.extensions = {}
LOGINGAMERES_RESULT_FIELD.name = "result"
LOGINGAMERES_RESULT_FIELD.full_name = ".com.kz.game.message.proto.LoginGameRes.result"
LOGINGAMERES_RESULT_FIELD.number = 1
LOGINGAMERES_RESULT_FIELD.index = 0
LOGINGAMERES_RESULT_FIELD.label = 2
LOGINGAMERES_RESULT_FIELD.has_default_value = false
LOGINGAMERES_RESULT_FIELD.default_value = 0
LOGINGAMERES_RESULT_FIELD.type = 5
LOGINGAMERES_RESULT_FIELD.cpp_type = 1

LOGINGAMERES_ROLEDATA_FIELD.name = "roleData"
LOGINGAMERES_ROLEDATA_FIELD.full_name = ".com.kz.game.message.proto.LoginGameRes.roleData"
LOGINGAMERES_ROLEDATA_FIELD.number = 2
LOGINGAMERES_ROLEDATA_FIELD.index = 1
LOGINGAMERES_ROLEDATA_FIELD.label = 1
LOGINGAMERES_ROLEDATA_FIELD.has_default_value = false
LOGINGAMERES_ROLEDATA_FIELD.default_value = nil
LOGINGAMERES_ROLEDATA_FIELD.message_type = ROLE_PB_ROLEBASEDATAPRO
LOGINGAMERES_ROLEDATA_FIELD.type = 11
LOGINGAMERES_ROLEDATA_FIELD.cpp_type = 10

LOGINGAMERES.name = "LoginGameRes"
LOGINGAMERES.full_name = ".com.kz.game.message.proto.LoginGameRes"
LOGINGAMERES.nested_types = {}
LOGINGAMERES.enum_types = {}
LOGINGAMERES.fields = {LOGINGAMERES_RESULT_FIELD, LOGINGAMERES_ROLEDATA_FIELD}
LOGINGAMERES.is_extendable = false
LOGINGAMERES.extensions = {}
CREATEROLEREQ_USERNAME_FIELD.name = "username"
CREATEROLEREQ_USERNAME_FIELD.full_name = ".com.kz.game.message.proto.CreateRoleReq.username"
CREATEROLEREQ_USERNAME_FIELD.number = 1
CREATEROLEREQ_USERNAME_FIELD.index = 0
CREATEROLEREQ_USERNAME_FIELD.label = 2
CREATEROLEREQ_USERNAME_FIELD.has_default_value = false
CREATEROLEREQ_USERNAME_FIELD.default_value = ""
CREATEROLEREQ_USERNAME_FIELD.type = 9
CREATEROLEREQ_USERNAME_FIELD.cpp_type = 9

CREATEROLEREQ_PASSWORD_FIELD.name = "password"
CREATEROLEREQ_PASSWORD_FIELD.full_name = ".com.kz.game.message.proto.CreateRoleReq.password"
CREATEROLEREQ_PASSWORD_FIELD.number = 2
CREATEROLEREQ_PASSWORD_FIELD.index = 1
CREATEROLEREQ_PASSWORD_FIELD.label = 2
CREATEROLEREQ_PASSWORD_FIELD.has_default_value = false
CREATEROLEREQ_PASSWORD_FIELD.default_value = ""
CREATEROLEREQ_PASSWORD_FIELD.type = 9
CREATEROLEREQ_PASSWORD_FIELD.cpp_type = 9

CREATEROLEREQ.name = "CreateRoleReq"
CREATEROLEREQ.full_name = ".com.kz.game.message.proto.CreateRoleReq"
CREATEROLEREQ.nested_types = {}
CREATEROLEREQ.enum_types = {}
CREATEROLEREQ.fields = {CREATEROLEREQ_USERNAME_FIELD, CREATEROLEREQ_PASSWORD_FIELD}
CREATEROLEREQ.is_extendable = false
CREATEROLEREQ.extensions = {}
CREATEROLERES_RESULT_FIELD.name = "result"
CREATEROLERES_RESULT_FIELD.full_name = ".com.kz.game.message.proto.CreateRoleRes.result"
CREATEROLERES_RESULT_FIELD.number = 1
CREATEROLERES_RESULT_FIELD.index = 0
CREATEROLERES_RESULT_FIELD.label = 2
CREATEROLERES_RESULT_FIELD.has_default_value = false
CREATEROLERES_RESULT_FIELD.default_value = 0
CREATEROLERES_RESULT_FIELD.type = 5
CREATEROLERES_RESULT_FIELD.cpp_type = 1

CREATEROLERES.name = "CreateRoleRes"
CREATEROLERES.full_name = ".com.kz.game.message.proto.CreateRoleRes"
CREATEROLERES.nested_types = {}
CREATEROLERES.enum_types = {}
CREATEROLERES.fields = {CREATEROLERES_RESULT_FIELD}
CREATEROLERES.is_extendable = false
CREATEROLERES.extensions = {}
MESSAGEERRORRES_RESULT_FIELD.name = "result"
MESSAGEERRORRES_RESULT_FIELD.full_name = ".com.kz.game.message.proto.MessageErrorRes.result"
MESSAGEERRORRES_RESULT_FIELD.number = 1
MESSAGEERRORRES_RESULT_FIELD.index = 0
MESSAGEERRORRES_RESULT_FIELD.label = 2
MESSAGEERRORRES_RESULT_FIELD.has_default_value = false
MESSAGEERRORRES_RESULT_FIELD.default_value = 0
MESSAGEERRORRES_RESULT_FIELD.type = 5
MESSAGEERRORRES_RESULT_FIELD.cpp_type = 1

MESSAGEERRORRES.name = "MessageErrorRes"
MESSAGEERRORRES.full_name = ".com.kz.game.message.proto.MessageErrorRes"
MESSAGEERRORRES.nested_types = {}
MESSAGEERRORRES.enum_types = {}
MESSAGEERRORRES.fields = {MESSAGEERRORRES_RESULT_FIELD}
MESSAGEERRORRES.is_extendable = false
MESSAGEERRORRES.extensions = {}

CreateRoleReq = protobuf.Message(CREATEROLEREQ)
CreateRoleRes = protobuf.Message(CREATEROLERES)
LoginGameReq = protobuf.Message(LOGINGAMEREQ)
LoginGameRes = protobuf.Message(LOGINGAMERES)
MessageErrorRes = protobuf.Message(MESSAGEERRORRES)

